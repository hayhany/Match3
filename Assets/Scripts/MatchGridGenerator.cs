public static class MatchGridGenerator
{
    public static event System.Action<byte[,]> OnGridGenerated;

    public static byte[,] GenerateGrid(int width, int height)
    {
        byte[,] grid = new byte[height,width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // TODO: implement an algorithm to place blocks in a manner that doesn't warrant matches on spawning
                grid[i,j] = MatchGridPool.Instance.GetRandomBlock().Identifier;
            }
        }

        OnGridGenerated?.Invoke(grid);
        return grid;
    }
}
