namespace AdventOfCode2021 {
    public static class Extensions {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action) {
            foreach (T item in enumeration) {
                action(item);
            }
        }

        public static int Count<T>(this T[,] arr, Func<T, bool> func) {
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    if (func(arr[i, j])) {
                        count += 1;
                    }
                }
            }
            return count;
        }
    }
}