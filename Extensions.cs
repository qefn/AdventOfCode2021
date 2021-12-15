namespace AdventOfCode2021 {
    public static class Extensions {
        public static void CopyTo<T>(this T[,] from, T[,] to, int index0, int index1, Func<T, T> copyElement) {
            int fromLength0 = from.GetLength(0);
            int fromLength1 = from.GetLength(1);
            Enumerable.Range(0, fromLength1)
                .ForEach(i => Enumerable.Range(0, fromLength1)
                                .ForEach(j => to[i + index0, j + index1] = copyElement(from[i, j])));
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

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action) {
            foreach (T item in enumeration) {
                action(item);
            }
        }

        public static bool IsInBounds<T>(this T[,] arr, int first, int second) {
            return first >= 0 && first < arr.GetLength(0) && second >= 0 && second < arr.GetLength(1);
        }
    }
}