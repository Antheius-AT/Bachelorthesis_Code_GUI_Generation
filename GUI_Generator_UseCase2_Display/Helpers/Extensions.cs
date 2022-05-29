
namespace GUI_Generator_UseCase1_Display.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<T> GetDescendantElementsOfType<T>(this IEnumerable<Type> source) where T : class
        {
            var filtered = source.Where(t => t.IsAssignableTo(typeof(T)) && t != typeof(T));

            foreach (var item in filtered)
            {
                var result = Activator.CreateInstance(item) as T;

                yield return result ?? throw new InvalidOperationException("Could not instantiate instance of type T");
            }
        }
    }
}
