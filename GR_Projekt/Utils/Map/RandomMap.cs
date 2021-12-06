using System;
using System.Collections.Generic;

namespace GR_Projekt.Utils.Map
{
    class RandomMap
    {
        private readonly int MAP_HEIGHT = 50;
        private readonly int MAP_WIDTH = 50;

        private readonly int STRONGHOLD_STEPS = 200;

        private readonly int LIVE_LIMIT = 4;
        private readonly int DEAD_LIMIT = 4;
        private readonly int STEPS = 20;
        private readonly float LIVE_CELL_CHANCE = 0.45f;

        public  List<int>[] baseMap;
        private List<int>[] map;
        private List<int>[] tempMap;
        private List<List<Block>> blockMap;

        private Random randomGenerator = new Random();

        public RandomMap()
        {
            generateInitMap();
            addInnerFloor1();
            addInnerFloor2();
            AddInnerFloor3();
            AddCorruption();
            AddStronghold();
            //this.map[0][0] = 4;
            generateBlockMap();
        }

        public List<int>[] getMap()
        {
            return map;
        }

        public List<List<Block>> getBlockMap()
        {
            return blockMap;
        }

        private new List<int>[] getZeroMap()
        {
            List<int>[] zeroMap = new List<int>[MAP_HEIGHT];
            for (int x = 0; x < MAP_HEIGHT; x++)
            {
                zeroMap[x] = new List<int>();
                for (int y = 0; y < MAP_WIDTH; y++)
                {
                    zeroMap[x].Add(0);
                }
            }

            return zeroMap;
        }

        private void addCellRect(int cellType, int x, int y, int width, int height)
        /*
            Dodaje w wyznaczona pozycje prostokat o danym typie o podanych wymiarach
        */
        {
            for (int xx = x; xx < height; xx++)
            {
                for (int yy = y; yy < width; yy++)
                {
                    if ((yy == y) || (y == width - 1) || (xx == x) || (x == height - 1))
                        map[xx][yy] = cellType;
                }
            }
        }

        private int getNeightboursCount(int cellType, int x, int y, bool countOutbounds = false, int area = 1)
        {
            int count = 0;
            for (int i = x - area; i <= x + area; i++)
            {
                for (int j = y - area; j <= y + area; j++)
                {
                    if ((j == y) && (i == x))
                        continue;

                    if (((i < 0) || (i > this.MAP_HEIGHT - area) || (j < 0) || (j > this.MAP_WIDTH - area)))
                    {
                        if (countOutbounds)
                            count += 1;
                    }
                    else if (map[i][j] == cellType)
                        count += 1;
                }
            }
            return count;
        }

        private void doStep()
        {
            for (int x = 0; x < this.MAP_HEIGHT; x++)
            {
                for (int y = 0; y < this.MAP_WIDTH; y++)
                {
                    if (this.map[x][y] == 1)
                    {
                        if (this.getNeightboursCount(1, x, y, true) < this.DEAD_LIMIT)
                            this.tempMap[x][y] = 0;
                        else
                            this.tempMap[x][y] = 1;
                    }
                    else
                    {
                        if (this.getNeightboursCount(1, x, y, true) > this.LIVE_LIMIT)
                            this.tempMap[x][y] = 1;
                        else
                            this.tempMap[x][y] = 0;
                    }
                }
            }
        }

        private void generateBlockMap()
        {
            blockMap = new List<List<Block>>(MAP_HEIGHT);
            for (int x = 0; x < MAP_HEIGHT; x++)
            {
                blockMap.Add(new List<Block>());
                for (int y = 0; y < MAP_WIDTH; y++)
                {
                    blockMap[x].Add(
                        new Block(getBlockType(this.map[x][y]), this.getWalls(x, y))
                        );
                }
            }
        }

        private List<bool> getWalls(int x, int y)
        {
            return new List<bool>
            {
                ShouldBeWallAt(x - 1, y),
                ShouldBeWallAt(x + 1, y),
                ShouldBeWallAt(x, y +1),
                ShouldBeWallAt(x, y -1),
            };
        }

        private bool ShouldBeWallAt(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return true;
            }

            if (x >= MAP_HEIGHT || y >= MAP_WIDTH)
            {
                return true;
            }
            return this.map[x][y] == 1;
        }

        public BlockType getBlockType(int blockType)
        {
            switch (blockType)
            {
                case 0:
                    return BlockType.Floor;
                case 1:
                    return BlockType.Wall;
                case 2:
                    return BlockType.FloorInner1;
                case 3:
                    return BlockType.FloorInner2;
                case 4:
                    return BlockType.Corruption;
                case 5:
                    return BlockType.Stronghold;
                case 6:
                    return BlockType.FloorInner3;
                default:
                    return BlockType.Fallback;
            }
        }


        private void generateInitMap()
        {
            map = new List<int>[MAP_HEIGHT];
            tempMap = new List<int>[MAP_HEIGHT];
            for (int x = 0; x < MAP_HEIGHT; x++)
            {
                map[x] = new List<int>();
                tempMap[x] = new List<int>();
                for (int y = 0; y < MAP_WIDTH; y++)
                {
                    if ((randomGenerator.Next() % 100) < (this.LIVE_CELL_CHANCE * 100))
                    {
                        map[x].Add(1);
                        tempMap[x].Add(1);
                    }

                    else
                    {
                        map[x].Add(0);
                        tempMap[x].Add(0);
                    }
                }
            }

            // Dodaje obramowanie wokol mapy zeby mapa byla zamknieta ...
            //this.addCellRect(0, 0, 0, this.MAP_WIDTH, this.MAP_HEIGHT);
            // ... i estetycznie wyglądala
            //this.addCellRect(0, 1, 1, this.MAP_WIDTH - 1, this.MAP_HEIGHT - 1);
            //this.addCellRect(0, 2, 2, this.MAP_WIDTH - 2, this.MAP_HEIGHT - 2);

            // Dodaje zywe komorki blisko krawedzi mapy co ma zapobiegac tworzeniu sie odizolowanych pomieszczen
            this.addCellRect(1, this.MAP_WIDTH * 1 / 6, this.MAP_HEIGHT * 1 / 6, this.MAP_WIDTH * 5 / 6, this.MAP_HEIGHT * 5 / 6);
            this.addCellRect(0, 10, 10, 30, 30);

            for (int i = 0; i < this.STEPS; i++)
            {
                this.doStep();
                this.map = this.tempMap;
            }
            this.baseMap = map;


            ///.WriteLine("0, 0 - 1 friends: " + this.getNeightboursCount(1, 1, 1).ToString());
            //Debug.WriteLine("0, 0 - 1 bounds friends: " + this.getNeightboursCount(1, 1, 1, true).ToString());
            //Debug.WriteLine("0, 0 - 0 friends: " + this.getNeightboursCount(0, 1, 1).ToString());
            //Debug.WriteLine("0, 0 - 0 bounds friends: " + this.getNeightboursCount(0, 1, 1, true).ToString());
        }

        private void addInnerFloor1()
        {
            List<int>[] localTempMap = getZeroMap();
            int maxNgCount = 0;

            for (int x = 0; x < this.MAP_HEIGHT; x++)
            {
                for (int y = 0; y < this.MAP_WIDTH; y++)
                {
                    int ngCount = this.getNeightboursCount(0, x, y, false);
                    if (ngCount > 7)
                    {
                        localTempMap[x][y] = 2;
                    }
                    else
                    {
                        localTempMap[x][y] = this.map[x][y];
                    }

                    if (ngCount > maxNgCount)
                    {
                        maxNgCount = ngCount;
                    }
                }
            }
            //Debug.WriteLine(maxNgCount);
            this.map = localTempMap;
        }

        private void addInnerFloor2()
        {
            List<int>[] localTempMap = getZeroMap();
            int maxNgCount = 0;

            for (int x = 0; x < this.MAP_HEIGHT; x++)
            {
                for (int y = 0; y < this.MAP_WIDTH; y++)
                {
                    int ngCount = this.getNeightboursCount(0, x, y, false, 3);
                    if (ngCount > 44)
                    {
                        localTempMap[x][y] = 3;
                    }
                    else
                    {
                        localTempMap[x][y] = this.map[x][y];
                    }

                    if (ngCount > maxNgCount)
                    {
                        maxNgCount = ngCount;
                    }
                }
            }
            //Debug.WriteLine(maxNgCount);
            this.map = localTempMap;
        }

        private void AddInnerFloor3()
        {
            List<int>[] localTempMap = getZeroMap();

            for (int x = 0; x < this.MAP_HEIGHT; x++)
            {
                for (int y = 0; y < this.MAP_WIDTH; y++)
                {
                    localTempMap[x][y] = this.map[x][y];
                }
            }

            List<int> nearestFloor = GetNearestBlockOfNotType(localTempMap, new List<int>() { 1, 5, 6 }, 25, 25);
            localTempMap[nearestFloor[0]][nearestFloor[1]] = 6;

            for (int i = 0; i < this.STRONGHOLD_STEPS; i++)
            {
                nearestFloor = GetNearestBlockOfNotType(localTempMap, new List<int>() { 1, 5, 6 }, nearestFloor[0], nearestFloor[1]);
                localTempMap[nearestFloor[0]][nearestFloor[1]] = 6;
            }
            this.map = localTempMap;

        }

        private List<int> GetNearestBlock(List<int>[] map, int type, int x, int y, int area = 1)
        {
            // Provides direction randomness
            int sign = randomGenerator.Next() % 2 == 0 ? 1 : -1;
            int x_vec = sign * randomGenerator.Next() % 3;
            int y_vec = sign * randomGenerator.Next() % 3;
            //Debug.WriteLine(x_vec + " - " + y_vec);

            if (randomGenerator.Next() % 2 == 1)
            {
                for (int i = x - area + x_vec; i <= x + area + x_vec; i++)
                {
                    for (int j = y - area + y_vec; j <= y + area + y_vec; j++)
                    {
                        if ((j == y) && (i == x))
                            continue;

                        if (((i < 0) || (i > this.MAP_HEIGHT - area) || (j < 0) || (j > this.MAP_WIDTH - area)))
                        {
                            continue;
                        }

                        if (map[i][j] == type)
                            return new List<int>() { i, j };
                    }
                }
            }
            else
            {
                for (int i = x + area; i > x - area; i--)
                {
                    for (int j = y + area; j > y - area; j--)
                    {
                        if ((j == y) && (i == x))
                            continue;

                        if (((i < 0) || (i > this.MAP_HEIGHT - area) || (j < 0) || (j > this.MAP_WIDTH - area)))
                        {
                            continue;
                        }

                        if (map[i][j] == type)
                            return new List<int>() { i, j };
                    }
                }
            }

                return GetNearestBlock(map, type, x, y, area + 1);
        }

        private List<int> GetNearestBlockOfNotType(List<int>[] map, List<int> types, int x, int y, int area = 1)
        {
            // Provides direction randomness
            int sign = randomGenerator.Next() % 2 == 0 ? 1 : -1;
            int x_vec = 0 * randomGenerator.Next() % 3;
            int y_vec = 0 * randomGenerator.Next() % 3;
            //Debug.WriteLine(x_vec + " - " + y_vec);

            if (randomGenerator.Next() % 2 == 1) {
                for (int i = x - area + x_vec; i <= x + area + x_vec; i++)
                {
                    for (int j = y - area + y_vec; j <= y + area + y_vec; j++)
                    {
                        if ((j == y) && (i == x))
                            continue;

                        if (((i < 0) || (i > this.MAP_HEIGHT - area) || (j < 0) || (j > this.MAP_WIDTH - area)))
                        {
                            continue;
                        }

                        if (!types.Contains(map[i][j]))
                        {
                            //Debug.WriteLine(map[i][j] + " Its fine: " + types);
                            return new List<int>() { i, j };
                        }
                    }
                }
            } else
            {
                for (int i = x + area; i > x - area; i--)
                {
                    for (int j = y + area; j > y - area; j--)
                    {
                        if ((j == y) && (i == x))
                            continue;

                        if (((i < 0) || (i > this.MAP_HEIGHT - area) || (j < 0) || (j > this.MAP_WIDTH - area)))
                        {
                            continue;
                        }

                        if (!types.Contains(map[i][j]))
                        {
                            //Debug.WriteLine(map[i][j] + " Its fine: " + types);
                            return new List<int>() { i, j };
                        }
                    }
                }
            }


            return GetNearestBlockOfNotType(map, types, x, y, area + 1);
        }

        private void AddCorruption()
        {
            List<int>[] localTempMap = getZeroMap();
            int maxNgCountFloor = 0;
            int maxNgCountWall = 0;

            for (int x = 0; x < this.MAP_HEIGHT; x++)
            {
                for (int y = 0; y < this.MAP_WIDTH; y++)
                {
                    int ngCountFloor = this.getNeightboursCount(0, x, y, false, 2);
                    int ngCountWall = this.getNeightboursCount(1, x, y, false, 2);
                    if (ngCountFloor == 10 && ngCountWall == 10)
                    {
                        localTempMap[x][y] = 4;

                        List<int> nearestFloor = GetNearestBlock(localTempMap, 1, x, y);
                        localTempMap[nearestFloor[0]][nearestFloor[1]] = 4;
                    }
                    else
                    {
                        localTempMap[x][y] = this.map[x][y];
                    }

                    if (ngCountFloor > maxNgCountFloor)
                    {
                        maxNgCountFloor = ngCountFloor;
                    }

                    if (ngCountWall > maxNgCountWall)
                    {
                        maxNgCountWall = ngCountWall;
                    }
                }
            }
            //Debug.WriteLine(maxNgCountFloor);
            //Debug.WriteLine(maxNgCountWall);
            this.map = localTempMap;
        }


        private void AddStronghold()
        {
            List<int>[] localTempMap = getZeroMap();

            for (int x = 0; x < this.MAP_HEIGHT; x++)
            {
                for (int y = 0; y < this.MAP_WIDTH; y++)
                {
                    localTempMap[x][y] = this.map[x][y];
                }
            }

            List<int> nearestFloor = GetNearestBlockOfNotType(localTempMap, new List<int> { 1, 5 }, 0, 0);
            localTempMap[nearestFloor[0]][nearestFloor[1]] = 5;

            for (int i = 0; i < this.STRONGHOLD_STEPS; i++)
            {
                nearestFloor = GetNearestBlockOfNotType(localTempMap, new List<int> { 1, 5 }, 0, 0);
                localTempMap[nearestFloor[0]][nearestFloor[1]] = 5;
            }
            this.map = localTempMap;
        }
    }
}
