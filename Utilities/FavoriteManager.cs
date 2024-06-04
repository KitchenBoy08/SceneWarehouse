using Newtonsoft.Json;

using System.Collections.Generic;

using MelonLoader;

using SceneWarehouse.Bonemenu;

namespace SceneWarehouse.Utilities
{
    internal class FavoriteManager
    {
        internal static readonly string SavePath = $"{MelonUtils.UserDataDirectory}/FavoriteLevels.json";

        internal static List<KeyValuePair<string, string>> FavoriteLevels = new List<KeyValuePair<string, string>>();

        public static bool IsFavorite(string levelBarcode, string title)
        {
            return FavoriteLevels.Contains(new KeyValuePair<string, string>(levelBarcode, title));
        }

        public static void AddFavorite(string levelBarcode, string title)
        {
            FavoriteLevels.Add(new KeyValuePair<string, string>(levelBarcode, title));

            SaveFavorites();
            FavoriteLevelsMenu.RefreshFavoriteList();
        }

        public static void RemoveFavorite(string levelBarcode, string title)
        {
            FavoriteLevels.Remove(new KeyValuePair<string, string>(levelBarcode, title));

            SaveFavorites();
            FavoriteLevelsMenu.RefreshFavoriteList();
        }

        public static void SaveFavorites()
        {
            string jsonText = JsonConvert.SerializeObject(FavoriteLevels);

            System.IO.File.WriteAllText(SavePath, jsonText);
        }

        public static List<KeyValuePair<string, string>> LoadFavoriteLevels()
        {
           if (System.IO.File.Exists(SavePath))
           {
                string jsonText = System.IO.File.ReadAllText(SavePath);

                return JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(jsonText);
           }

            return new List<KeyValuePair<string, string>>();
        }
    }
}
