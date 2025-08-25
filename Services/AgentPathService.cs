using Pathfinding.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    // Change FindPath from static to instance method, and use the injected _levelGeneratorService instance.
    // Also remove the static Instance field, as Zenject will handle instantiation.

    [DefaultExecutionOrder(100)]

    public class AgentPathService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private AgentMoveService _agentMoveService;

        /// <summary>
        /// Represents a node in the pathfinding graph, storing position, parent, and cost values.
        /// </summary>
        public class Node
        {
            public Vector2Int position; // Position of the node on the grid
            public Node parent; // Parent node in the path
            public int gCost; // Cost from start node
            public int hCost; // Heuristic cost to goal
            public int FCost => gCost + hCost; // Total cost

            public Node(Vector2Int pos, Node parent, int gCost, int hCost)
            {
                this.position = pos;
                this.parent = parent;
                this.gCost = gCost;
                this.hCost = hCost;
            }
        }

        public void InitPatrol(AgentData enemy)
        {
            enemy.AIData.PointA = _levelUtilityService.GetGridPosition(enemy.MovementData.BodyTransform.position);

            var validPositions = _levelGeneratorService.Data.ValidGridPositions;
            int minDistance = 8;
            int maxDistance = 24;

            var patrolCandidates = validPositions.FindAll(pos =>
            {
                int dist = Mathf.Abs(pos.x - enemy.AIData.PointA.x) + Mathf.Abs(pos.y - enemy.AIData.PointA.y);
                return dist >= minDistance && dist <= maxDistance;
            });

            if (patrolCandidates.Count == 0)
            {
                patrolCandidates = validPositions;
            }

            Vector2Int chosenPointB = enemy.AIData.PointA;
            foreach (var candidate in patrolCandidates)
            {
                enemy.MovementData.TargetPos = candidate;
                var path = FindPath(enemy.MovementData);
                if (path != null && path.Count > 0)
                {
                    chosenPointB = candidate;
                    break;
                }
            }

            enemy.AIData.PointB = chosenPointB;
            enemy.MovementData.TargetPos = enemy.AIData.PointB;
            _agentMoveService.StartAction(enemy);
        }

        /// <summary>
        /// Finds the shortest path from start to goal using the A* algorithm.
        /// </summary>
        /// <param name="start">Starting grid position</param>
        /// <param name="goal">Goal grid position</param>
        /// <param name="isWalkable">Function to determine if a grid position is walkable</param>
        /// <param name="gridWidth">Width of the grid</param>
        /// <param name="gridHeight">Height of the grid</param>
        /// <returns>List of grid positions representing the path, or null if no path found</returns>
        public List<Vector2Int> FindPath(MovementData data)
        {
            int gridWidth = _levelGeneratorService.Data.Width;
            int gridHeight = _levelGeneratorService.Data.Height;
            Vector2Int start = _levelUtilityService.GetGridPosition(data.Rb.transform.position);

            var openSet = new List<Node>(); // Nodes to be evaluated
            var closedSet = new HashSet<Vector2Int>(); // Evaluated nodes

            Node startNode = new Node(start, null, 0, GetHeuristic(start, data.TargetPos));
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Find node with lowest F cost
                Node current = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < current.FCost ||
                        (openSet[i].FCost == current.FCost && openSet[i].hCost < current.hCost))
                    {
                        current = openSet[i];
                    }
                }

                // If goal reached, reconstruct and return path
                if (current.position.Equals(data.TargetPos))
                {
                    return ReconstructPath(current);
                }

                openSet.Remove(current);
                closedSet.Add(current.position);

                // Evaluate neighbors
                foreach (var neighbor in GetNeighbors(current.position, gridWidth, gridHeight))
                {
                    bool isWalkable = _levelUtilityService.IsWalkable(neighbor);

                    if (!isWalkable || closedSet.Contains(neighbor)) continue;

                    // Default cost for normal tiles
                    int moveCost = 1;

                    // Check if neighbor is a forest tile
                    var gridSquare = _levelGeneratorService.Data.Squares[neighbor.x, neighbor.y];
                    if (gridSquare.GridSquareType.Contains(GridSquareType.Forest))
                    {
                        moveCost = _levelGeneratorService.Data.ForestCost;
                    }

                    int tentativeGCost = current.gCost + moveCost;

                    Node neighborNode = openSet.Find(n => n.position.Equals(neighbor));
                    if (neighborNode == null)
                    {
                        neighborNode = new Node(neighbor, current, tentativeGCost, GetHeuristic(neighbor, data.TargetPos));
                        openSet.Add(neighborNode);
                    }
                    else if (tentativeGCost < neighborNode.gCost)
                    {
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.parent = current;
                    }
                }
            }

            // No path found
            return null;
        }

        /// <summary>
        /// Reconstructs the path from the goal node to the start node.
        /// </summary>
        private static List<Vector2Int> ReconstructPath(Node node)
        {
            var path = new List<Vector2Int>();
            while (node != null)
            {
                path.Add(node.position);
                node = node.parent;
            }
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Calculates the Manhattan distance heuristic between two grid positions.
        /// </summary>
        private static int GetHeuristic(Vector2Int a, Vector2Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        /// <summary>
        /// Returns the neighboring grid positions (up, down, left, right) within grid bounds.
        /// </summary>
        private static IEnumerable<Vector2Int> GetNeighbors(Vector2Int pos, int gridWidth, int gridHeight)
        {
            var directions = new[]
            {
                new Vector2Int(1, 0),    // Right
                new Vector2Int(-1, 0),   // Left
                new Vector2Int(0, 1),    // Up
                new Vector2Int(0, -1)    // Down
            };

            foreach (var dir in directions)
            {
                Vector2Int neighbor = new Vector2Int(pos.x + dir.x, pos.y + dir.y);
                if (neighbor.x >= 0 && neighbor.x < gridWidth && neighbor.y >= 0 && neighbor.y < gridHeight)
                {
                    yield return neighbor;
                }
            }
        }
    }
}