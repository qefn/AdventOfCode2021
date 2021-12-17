using System.Drawing;

namespace AdventOfCode2021.Solutions {
    public class Day17Solution : Solution {
        public override List<Action> Stages => new List<Action> { Solve };

        private void Solve() {
            string input = ReadInputFile("Day17Stage01.txt").First();
            Rectangle targetArea = ParseTargetArea(input);
            int maxVelocity = 100;
            int maxHeight = 0;
            int hitcount = 0;
            //rectangles with negative y coordinates are weird. Top and bottom are reversed
            for (int y = targetArea.Top - 1; y < maxVelocity; y++) {
                for (int x = 0; x < targetArea.Right + 1; x++) {
                    Drone drone = new Drone(x, y, targetArea);
                    bool hit = drone.Launch(out int height);
                    if (hit) {
                        hitcount++;
                        if (height > maxHeight) {
                            maxHeight = height;
                        }
                    }
                }
            }

            Console.WriteLine($"Max height reached: {maxHeight}");
            Console.WriteLine($"Nr. of drones hitting the area: {hitcount}");
        }

        private Rectangle ParseTargetArea(string input) {
            int commaIndex = input.IndexOf(",");
            int firstEqualsIndex = input.IndexOf("=");
            int lastEqualsIndex = input.LastIndexOf("=");
            string leftRight = input.Substring(firstEqualsIndex + 1, commaIndex - firstEqualsIndex - 1);
            string topBottom = input.Substring(lastEqualsIndex + 1);
            int left = int.Parse(leftRight.Split("..")[0]);
            int right = int.Parse(leftRight.Split("..")[1]);
            int top = int.Parse(topBottom.Split("..")[1]);
            int bottom = int.Parse(topBottom.Split("..")[0]);
            return new Rectangle(left, bottom, right - left + 1, Math.Abs(bottom - top) + 1);
        }

        private class Drone {
            public Drone(int velocityX, int velocityY, Rectangle targetArea) {
                VelocityX = velocityX;
                VelocityY = velocityY;
                TargetArea = targetArea;
            }

            private int X { get; set; } = 0;
            private int Y { get; set; } = 0;
            private int VelocityX { get; set; }
            private int VelocityY { get; set; }
            private Rectangle TargetArea { get; set; }
            public bool Launch(out int maxHeight) {
                maxHeight = 0;
                bool hitTarget = false;
                while (X <= TargetArea.Right && Y >= TargetArea.Top) {
                    X += VelocityX;
                    Y += VelocityY;
                    if (Y > maxHeight) {
                        maxHeight = Y;
                    }
                    VelocityX = VelocityX == 0 ? 0 : VelocityX < 0 ? VelocityX + 1 : VelocityX - 1;
                    VelocityY -= 1;
                    hitTarget |= TargetArea.Contains(X, Y);
                }
                return hitTarget;
            }
        }
    }
}