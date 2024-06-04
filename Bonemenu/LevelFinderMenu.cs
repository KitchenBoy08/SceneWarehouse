using BoneLib.BoneMenu.Elements;

using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using SLZ.Marrow.Warehouse;

namespace SceneWarehouse.Bonemenu
{
    internal class LevelFinderMenu
    {
        public enum SortMode
        {
            None,
            Pallet,
            Author,
        }

        internal static MenuCategory LevelFinderCategory;

        internal static EnumElement<SortMode> SortModeElement;
        internal static IntElement PageNumberElement;

        internal static void CreateLevelFinder(MenuCategory category)
        {
            LevelFinderCategory = category.CreateCategory("Level Finder", Color.cyan);

            SortModeElement = LevelFinderCategory.CreateEnumElement<SortMode>("Sort Levels By", Color.grey, (sortMode) =>
            {
                _pageSortMode = sortMode;
                PageNumberElement.SetValue(1);
                PageNumber = 1;
                RefreshLevelList();
            });
            PageNumberElement = LevelFinderCategory.CreateIntElement("Page Number", Color.grey, 1, 1, 1, int.MaxValue, (pageNumber) =>
            {
                PageNumber = pageNumber;
                RefreshLevelList();
            });
        }

        internal static List<Crate> Crates;

        private static List<MenuElement> _levelElementList = new List<MenuElement>();
        public static int PageNumber { get; private set; } = 1;
        private static SortMode _pageSortMode = SortMode.None;
        internal static void RefreshLevelList()
        {
            using (BatchedBoneMenu.Create())
            {
                foreach (var element in _levelElementList)
                {
                    LevelFinderCategory.RemoveElement(element);
                }

                _levelElementList.Clear();

                switch (_pageSortMode)
                {
                    case SortMode.None:
                        {
                            List<Crate> levelCrates = new List<Crate>();

                            foreach (var crate in Crates)
                            {
                                if (crate == null || crate.Redacted || !crate.TryCast<LevelCrate>())
                                    continue;

                                levelCrates.Add(crate);
                            }
                            levelCrates = levelCrates.OrderBy(x => x.Title).ToList();

                            for (int i = (PageNumber - 1) * 5; i < ((PageNumber - 1) * 5) + 5; i++)
                            {
                                if (i >= levelCrates.Count)
                                    break;

                                var crate = levelCrates[i];
                                if (crate == null)
                                    continue;

                                _levelElementList.Add(BonemenuManager.CreateLevelCategory(LevelFinderCategory, crate));
                            }

                            break;
                        }
                    case SortMode.Pallet:
                        {
                            List<string> levelPallets = new List<string>();

                            foreach (var crate in Crates)
                            {
                                if (crate == null || crate.Redacted || !crate.TryCast<LevelCrate>() || levelPallets.Contains(crate.Pallet.Title))
                                    continue;

                                levelPallets.Add(crate.Pallet.Title);
                            }
                            levelPallets = levelPallets.OrderBy(x => x).ToList();

                            for (int i = (PageNumber - 1) * 5; i < ((PageNumber - 1) * 5) + 5; i++)
                            {
                                if (i >= levelPallets.Count)
                                    break;

                                var pallet = levelPallets[i];
                                if (pallet == null)
                                    continue;

                                var palletCategory = LevelFinderCategory.CreateCategory(pallet, Color.white);
                                _levelElementList.Add(palletCategory);

                                foreach (var crate in Crates.Where(x => x.Pallet.Title == pallet))
                                {
                                    if (crate == null || crate.Redacted || !crate.TryCast<LevelCrate>())
                                        continue;

                                    BonemenuManager.CreateLevelCategory(palletCategory, crate);
                                }
                            }

                            break;
                        }
                    case SortMode.Author:
                        {
                            List<string> levelAuthors = new List<string>();

                            foreach (var crate in Crates)
                            {
                                if (crate == null || crate.Redacted || !crate.TryCast<LevelCrate>() || levelAuthors.Contains(crate.Pallet.Author))
                                    continue;

                                levelAuthors.Add(crate.Pallet.Author);
                            }
                            levelAuthors = levelAuthors.OrderBy(x => x).ToList();

                            for (int i = (PageNumber - 1) * 5; i < ((PageNumber - 1) * 5) + 5; i++)
                            {
                                if (i >= levelAuthors.Count)
                                    break;

                                var author = levelAuthors[i];
                                if (author == null)
                                    continue;

                                var authorCategory = LevelFinderCategory.CreateCategory(author, Color.white);
                                _levelElementList.Add(authorCategory);

                                foreach (var crate in Crates.Where(x => x.Pallet.Author == author))
                                {
                                    if (crate == null || crate.Redacted || !crate.TryCast<LevelCrate>())
                                        continue;

                                    BonemenuManager.CreateLevelCategory(authorCategory, crate);
                                }
                            }

                            break;
                        }
                }
            }
        }

        public static void Reset()
        {
            if (PageNumber == 0)
                return;

            PageNumber = 1;
            _pageSortMode = SortMode.None;
            PageNumberElement.SetValue(1);
            SortModeElement.SetValue(SortMode.None);

            RefreshLevelList();
        }
    }
}
