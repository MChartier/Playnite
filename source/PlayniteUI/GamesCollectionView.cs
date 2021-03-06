﻿using NLog;
using Playnite;
using Playnite.Database;
using Playnite.SDK.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Playnite.SDK;

namespace PlayniteUI
{
    public class CategoryView : IComparable
    {
        public string Category
        {
            get; set;
        }

        public CategoryView(string category)
        {
            Category = category;
        }

        public int CompareTo(object obj)
        {
            var cat = (obj as CategoryView).Category;

            if (string.IsNullOrEmpty(Category) && string.IsNullOrEmpty(cat))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(Category))
            {
                return 1;
            }
            if (string.IsNullOrEmpty(cat))
            {
                return -1;
            }
            if (Category.Equals(cat))
            {
                return 0;
            }

            return string.Compare(Category, cat, true);
        }

        public override bool Equals(object obj)
        {
            var cat = ((CategoryView)obj).Category;

            if (string.IsNullOrEmpty(Category) && string.IsNullOrEmpty(cat))
            {
                return true;
            }
            if (string.IsNullOrEmpty(Category))
            {
                return false;
            }
            if (string.IsNullOrEmpty(cat))
            {
                return false;
            }
            if (Category.Equals(cat))
            {
                return true;
            }

            return string.Compare(Category, cat, true) == 0;
        }

        public override int GetHashCode()
        {
            if (Category == null)
            {
                return 0;
            }
            else
            {
                return Category.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Category) ? "No Category" : Category;
        }
    }

    public class PlatformView : IComparable
    {
        public Platform Platform
        {
            get; set;
        }

        public LiteDB.ObjectId PlatformId
        {
            get; set;
        }

        public string Name
        {
            get => Platform?.Name ?? string.Empty;
        }

        public PlatformView(LiteDB.ObjectId platformId, Platform platform)
        {
            Platform = platform;
            PlatformId = platformId;
        }

        public int CompareTo(object obj)
        {
            var platform = (obj as PlatformView).Name;

            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(platform))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(Name))
            {
                return 1;
            }
            if (string.IsNullOrEmpty(platform))
            {
                return -1;
            }
            if (Name.Equals(platform))
            {
                return 0;
            }

            return string.Compare(Name, platform, true);
        }

        public override bool Equals(object obj)
        {
            var platform = (obj as PlatformView).Name;

            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(platform))
            {
                return true;
            }
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(platform))
            {
                return false;
            }
            if (Name.Equals(platform))
            {
                return true;
            }

            return string.Compare(Name, platform, true) == 0;
        }

        public override int GetHashCode()
        {
            if (Name == null)
            {
                return 0;
            }
            else
            {
                return Name.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "No Platform" : Name;
        }
    }

    public class GameViewEntry : INotifyPropertyChanged
    {
        public int Id => Game.Id;
        public Provider Provider => Game.Provider;
        public string ProviderId => Game.ProviderId;
        public List<string> Categories => Game.Categories;
        public List<string> Tags => Game.Tags;
        public List<string> Genres => Game.Genres;
        public DateTime? ReleaseDate => Game.ReleaseDate;
        public DateTime? LastActivity => Game.LastActivity;
        public List<string> Developers => Game.Developers;
        public List<string> Publishers => Game.Publishers;
        public ObservableCollection<Link> Links => Game.Links;
        public string Icon => Game.Icon;
        public string Image => Game.Image;
        public string BackgroundImage => Game.BackgroundImage;
        public bool IsInstalled => Game.IsInstalled;
        public bool Hidden => Game.Hidden;
        public bool Favorite => Game.Favorite;
        public string InstallDirectory => Game.InstallDirectory;
        public LiteDB.ObjectId PlatformId => Game.PlatformId;
        public ObservableCollection<GameTask> OtherTasks => Game.OtherTasks;
        public string DisplayName => Game.Name;                
        public string Description => Game.Description;
        public bool IsInstalling => Game.IsInstalling;
        public bool IsUnistalling => Game.IsUninstalling;
        public bool IsLaunching => Game.IsLaunching;
        public bool IsRunning => Game.IsRunning;
        public GameState State => Game.State;
        public long Playtime => Game.Playtime;
        public DateTime? Added => Game.Added;
        public DateTime? Modified => Game.Modified;
        public long PlayCount => Game.PlayCount;
        public string Series => Game.Series;
        public string Version => Game.Version;
        public string AgeRating => Game.AgeRating;
        public string Region => Game.Region;
        public string Source => Game.Source;
        public CompletionStatus CompletionStatus => Game.CompletionStatus;
        public int? UserScore => Game.UserScore;
        public int? CriticScore => Game.CriticScore;
        public int? CommunityScore => Game.CommunityScore;

        public object IconObject => GetImageObject(Game.Icon);
        public object ImageObject => GetImageObject(Game.Image);
        public object BackgroundImageObject => GetImageObject(Game.BackgroundImage);
        public object DefaultIconObject => GetImageObject(DefaultIcon);
        public object DefaultImageObject => GetImageObject(DefaultImage);


        public string Name
        {
            get
            {
                return (string.IsNullOrEmpty(Game.SortingName)) ? Game.Name : Game.SortingName;
            }
        }

        public CategoryView Category
        {
            get; set;
        }

        private PlatformView platform;
        public PlatformView Platform
        {
            get => platform;
            set
            {
                platform = value;
                OnPropertyChanged("PlatformId");
            }
        }

        public Game Game
        {
            get; set;
        }

        public string DefaultIcon
        {
            get
            {
                if (!string.IsNullOrEmpty(Platform?.Platform?.Icon))
                {
                    return Platform.Platform.Icon;
                }
                else
                {
                    switch (Game.Provider)
                    {
                        case Provider.GOG:
                            return @"resources:/Images/gogicon.png";
                        case Provider.Origin:
                            return @"resources:/Images/originicon.png";
                        case Provider.Steam:
                            return @"resources:/Images/steamicon.png";
                        case Provider.Uplay:
                            return @"resources:/Images/uplayicon.png";
                        case Provider.BattleNet:
                            return @"resources:/Images/battleneticon.png";
                        case Provider.Custom:
                        default:
                            return @"resources:/Images/icon_dark.png";
                    }
                }
            }
        }

        public string DefaultImage
        {
            get
            {
                if (!string.IsNullOrEmpty(Platform?.Platform?.Cover))
                {
                    return Platform.Platform.Cover;
                }
                else
                {
                    switch (Game.Provider)
                    {
                        case Provider.GOG:
                        case Provider.Origin:
                        case Provider.Steam:
                        case Provider.Uplay:
                        case Provider.Custom:
                        default:
                            return @"resources:/Images/custom_cover_background.png";
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewEntry(Game game, string category, Platform platform)
        {
            Category = new CategoryView(category);
            Game = game;
            Game.PropertyChanged += Game_PropertyChanged;
            Platform = new PlatformView(PlatformId, platform);
        }

        private void Game_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == "PlatformId")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Platform"));
            }

            if (propertyName == "SortingName" || propertyName == "Name")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
            }

            if (propertyName == "Icon")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IconObject"));
            }

            if (propertyName == "Image")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageObject"));
            }

            if (propertyName == "BackgroundImage")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundImageObject"));
            }
        }

        private object GetImageObject(string data)
        {
            return CustomImageStringToImageConverter.GetImageFromSource(data);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Name, Category);
        }

        public static explicit operator Game(GameViewEntry entry)
        {
            return entry.Game;
        }
    }

    public enum GamesViewType
    {
        Standard,
        CategoryGrouped
    }

    public class GamesCollectionView : ObservableObject, IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private GameDatabase database;
        private List<Platform> platformsCache;

        public bool IsFullscreen
        {
            get; private set;
        }            

        private ListCollectionView collectionView;
        public ListCollectionView CollectionView
        {
            get => collectionView;
            private set
            {
                collectionView = value;
                OnPropertyChanged("CollectionView");
            }
        }

        public Settings Settings
        {
            get;
            private set;
        }

        private GamesViewType? viewType = null;
        public GamesViewType? ViewType
        {
            get => viewType;
            set
            {
                if (value == viewType)
                {
                    return;
                }

                SetViewType(value);
                viewType = value;
            }
        }

        public RangeObservableCollection<GameViewEntry> Items
        {
            get; set;
        }

        public GamesCollectionView(GameDatabase database, Settings settings, bool fullScreen)
        {
            IsFullscreen = fullScreen;
            this.database = database;
            platformsCache = database.PlatformsCollection.FindAll().ToList();
            database.GamesCollectionChanged += Database_GamesCollectionChanged;
            database.GameUpdated += Database_GameUpdated;
            database.PlatformsCollectionChanged += Database_PlatformsCollectionChanged;
            database.PlatformUpdated += Database_PlatformUpdated;
            Items = new RangeObservableCollection<GameViewEntry>();
            Settings = settings;
            if (IsFullscreen)
            {
                Settings.FullscreenViewSettings.PropertyChanged += Settings_PropertyChanged;
                Settings.FullScreenFilterSettings.FilterChanged += FilterSettings_FilterChanged;
            }
            else
            {
                Settings.ViewSettings.PropertyChanged += Settings_PropertyChanged;
                Settings.FilterSettings.FilterChanged += FilterSettings_FilterChanged;
            }

            CollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(Items);
            CollectionView.Filter = Filter;
            SetViewConfiguration();
        }

        public void Dispose()
        {
            database.GamesCollectionChanged -= Database_GamesCollectionChanged;
            database.GameUpdated -= Database_GameUpdated;
            database.PlatformsCollectionChanged -= Database_PlatformsCollectionChanged;
            database.PlatformUpdated -= Database_PlatformUpdated;
            Settings.PropertyChanged -= Settings_PropertyChanged;
            if (IsFullscreen)
            {
                Settings.FullscreenViewSettings.PropertyChanged -= Settings_PropertyChanged;
                Settings.FullScreenFilterSettings.FilterChanged -= FilterSettings_FilterChanged;
            }
            else
            {
                Settings.ViewSettings.PropertyChanged -= Settings_PropertyChanged;
                Settings.FilterSettings.FilterChanged -= FilterSettings_FilterChanged;
            }
        }

        private bool Filter(object item)
        {
            var entry = (GameViewEntry)item;
            var game = entry.Game;
            var filterSettings = IsFullscreen ? Settings.FullScreenFilterSettings : Settings.FilterSettings;

            if (!filterSettings.Active)
            {
                if (game.Hidden)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            // ------------------ Installed
            bool installedResult = false;
            if ((filterSettings.IsInstalled && filterSettings.IsUnInstalled) ||
                (!filterSettings.IsInstalled && !filterSettings.IsUnInstalled))
            {
                installedResult = true;
            }
            else
            {
                if (filterSettings.IsInstalled && game.IsInstalled)
                {
                    installedResult = true;
                }
                else if (filterSettings.IsUnInstalled && !game.IsInstalled)
                {
                    installedResult = true;
                }
            }

            // ------------------ Hidden
            bool hiddenResult = true;
            if (filterSettings.Hidden && game.Hidden)
            {
                hiddenResult = true;
            }
            else if (!filterSettings.Hidden && game.Hidden)
            {
                hiddenResult = false;
            }
            else if (filterSettings.Hidden && !game.Hidden)
            {
                hiddenResult = false;
            }

            // ------------------ Favorite
            bool favoriteResult = false;
            if (filterSettings.Favorite && game.Favorite)
            {
                favoriteResult = true;
            }
            else if (!filterSettings.Favorite)
            {
                favoriteResult = true;
            }

            // ------------------ Providers
            bool providersFilter = false;
            if (filterSettings.Steam == false &&
                filterSettings.Origin == false &&
                filterSettings.GOG == false &&
                filterSettings.Custom == false &&
                filterSettings.Uplay == false &&
                filterSettings.BattleNet == false)
            {
                providersFilter = true;
            }
            else
            {
                switch (game.Provider)
                {
                    case Provider.Custom:
                        if (filterSettings.Custom)
                        {
                            providersFilter = true;
                        }
                        break;
                    case Provider.GOG:
                        if (filterSettings.GOG)
                        {
                            providersFilter = true;
                        }
                        break;
                    case Provider.Origin:
                        if (filterSettings.Origin)
                        {
                            providersFilter = true;
                        }
                        break;
                    case Provider.Steam:
                        if (filterSettings.Steam)
                        {
                            providersFilter = true;
                        }
                        break;
                    case Provider.Uplay:
                        if (filterSettings.Uplay)
                        {
                            providersFilter = true;
                        }
                        break;
                    case Provider.BattleNet:
                        if (filterSettings.BattleNet)
                        {
                            providersFilter = true;
                        }
                        break;
                }
            }

            // ------------------ Name filter
            bool nameResult = false;
            if (string.IsNullOrEmpty(filterSettings.Name))
            {
                nameResult = true;
            }
            else
            {
                if (string.IsNullOrEmpty(game.Name))
                {
                    nameResult = false;
                }
                else
                {
                    nameResult = (game.Name.IndexOf(filterSettings.Name, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            // ------------------ Series filter
            bool seriesResult = false;
            if (string.IsNullOrEmpty(filterSettings.Series))
            {
                seriesResult = true;
            }
            else
            {
                if (string.IsNullOrEmpty(game.Series))
                {
                    seriesResult = false;
                }
                else
                {
                    seriesResult = (game.Series.IndexOf(filterSettings.Series, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            // ------------------ Region filter
            bool regionResult = false;
            if (string.IsNullOrEmpty(filterSettings.Region))
            {
                regionResult = true;
            }
            else
            {
                if (string.IsNullOrEmpty(game.Region))
                {
                    regionResult = false;
                }
                else
                {
                    regionResult = (game.Region.IndexOf(filterSettings.Region, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            // ------------------ Source filter
            bool sourceResult = false;
            if (string.IsNullOrEmpty(filterSettings.Source))
            {
                sourceResult = true;
            }
            else
            {
                if (string.IsNullOrEmpty(game.Source))
                {
                    sourceResult = false;
                }
                else
                {
                    sourceResult = (game.Source.IndexOf(filterSettings.Source, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            // ------------------ AgeRating filter
            bool ageRatingResult = false;
            if (string.IsNullOrEmpty(filterSettings.AgeRating))
            {
                ageRatingResult = true;
            }
            else
            {
                if (string.IsNullOrEmpty(game.AgeRating))
                {
                    ageRatingResult = false;
                }
                else
                {
                    ageRatingResult = (game.AgeRating.IndexOf(filterSettings.AgeRating, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            // ------------------ Genre
            bool genreResult = false;
            if (filterSettings.Genres == null || filterSettings.Genres.Count == 0)
            {
                genreResult = true;
            }
            else
            {
                if (game.Genres == null)
                {
                    genreResult = false;
                }
                else
                {
                    genreResult = filterSettings.Genres.IntersectsPartiallyWith(game.Genres);
                }
            }

            // ------------------ Platform
            bool platformResult = false;
            if (filterSettings.Platforms == null || filterSettings.Platforms.Count == 0)
            {
                platformResult = true;
            }
            else
            {
                if (game.PlatformId == null)
                {
                    platformResult = false;
                }
                else
                {
                    var platform = GetPlatformFromCache(game.PlatformId);
                    if (platform == null)
                    {
                        platformResult = false;
                    }
                    else
                    {
                        platformResult = filterSettings.Platforms.Any(a => !string.IsNullOrEmpty(a) && platform.Name.IndexOf(a, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }
            }

            // ------------------ Release Date
            bool releaseDateResult = false;
            if (string.IsNullOrEmpty(filterSettings.ReleaseDate))
            {
                releaseDateResult = true;
            }
            else
            {
                if (game.ReleaseDate == null)
                {

                    releaseDateResult = false;
                }
                else
                {
                    releaseDateResult = game.ReleaseDate.Value.ToString(Constants.DateUiFormat).IndexOf(filterSettings.ReleaseDate, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }

            // ------------------ Publisher
            bool publisherResult = false;
            if (filterSettings.Publishers == null || filterSettings.Publishers.Count == 0)
            {
                publisherResult = true;
            }
            else
            {
                if (game.Publishers == null)
                {
                    publisherResult = false;
                }
                else
                {
                    publisherResult = filterSettings.Publishers.IntersectsPartiallyWith(game.Publishers);
                }
            }

            // ------------------ Developer
            bool developerResult = false;
            if (filterSettings.Developers == null || filterSettings.Developers.Count == 0)
            {
                developerResult = true;
            }
            else
            {
                if (game.Developers == null)
                {
                    developerResult = false;
                }
                else
                {
                    developerResult = filterSettings.Developers.IntersectsPartiallyWith(game.Developers);
                }
            }

            // ------------------ Category
            bool categoryResult = false;
            if (filterSettings.Categories == null || filterSettings.Categories.Count == 0)
            {
                categoryResult = true;
            }
            else
            {
                if (game.Categories == null)
                {
                    categoryResult = false;
                }
                else
                {
                    if (ViewType == GamesViewType.Standard)
                    {
                        categoryResult = filterSettings.Categories.IntersectsPartiallyWith(game.Categories);
                    }
                    else
                    {
                        categoryResult = filterSettings.Categories.Any(a => entry.Category.Category.IndexOf(a, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }
            }

            // ------------------ Tags
            bool tagResult = false;
            if (filterSettings.Tags == null || filterSettings.Tags.Count == 0)
            {
                tagResult = true;
            }
            else
            {
                if (game.Tags == null)
                {
                    tagResult = false;
                }
                else
                {
                    tagResult = filterSettings.Tags.IntersectsPartiallyWith(game.Tags);
                }
            }

            return installedResult &&
                hiddenResult &&
                favoriteResult &&
                nameResult &&
                providersFilter &&
                genreResult &&
                platformResult &&
                releaseDateResult &&
                publisherResult &&
                developerResult &&
                categoryResult &&
                tagResult &&
                seriesResult &&
                regionResult &&
                sourceResult &&
                ageRatingResult;
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((new string[] { "SortingOrder", "GroupingOrder", "SortingOrderDirection" }).Contains(e.PropertyName))
            {
                UpdateViewConfiguration();
            }
        }

        private void FilterSettings_FilterChanged(object sender, FilterChangedEventArgs e)
        {
            logger.Debug("Refreshing collection view filter.");
            CollectionView.Refresh();
        }

        private void SetViewDescriptions()
        {
            ViewSettings viewSettings = IsFullscreen ? Settings.FullscreenViewSettings : Settings.ViewSettings;            
            var sortDirection = viewSettings.SortingOrderDirection == SortOrderDirection.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;

            if (IsFullscreen)
            {
                ViewType = GamesViewType.Standard;
            }
            else
            {
                switch (viewSettings.GroupingOrder)
                {
                    case GroupOrder.None:
                        ViewType = GamesViewType.Standard;
                        break;
                    case GroupOrder.Provider:
                        ViewType = GamesViewType.Standard;
                        break;
                    case GroupOrder.Platform:
                        ViewType = GamesViewType.Standard;
                        break;
                    case GroupOrder.Category:
                        ViewType = GamesViewType.CategoryGrouped;
                        break;
                }
            }

            if (viewSettings.SortingOrder == SortOrder.Name)
            {
                sortDirection = sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }

            CollectionView.SortDescriptions.Add(new SortDescription(viewSettings.SortingOrder.ToString(), sortDirection));
            if (viewSettings.SortingOrder != SortOrder.Name)
            {
                CollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            if (viewSettings.GroupingOrder != GroupOrder.None)
            {
                CollectionView.GroupDescriptions.Add(new PropertyGroupDescription(viewSettings.GroupingOrder.ToString()));
                if (CollectionView.SortDescriptions.First().PropertyName != viewSettings.GroupingOrder.ToString())
                {
                    CollectionView.SortDescriptions.Insert(0, new SortDescription(viewSettings.GroupingOrder.ToString(), ListSortDirection.Ascending));
                }
            }
        }

        private void SetViewConfiguration()
        {
            using (CollectionView.DeferRefresh())
            {
                SetViewDescriptions();

                CollectionView.LiveGroupingProperties.Add("Provider");
                CollectionView.LiveGroupingProperties.Add("Category");
                CollectionView.LiveGroupingProperties.Add("Platform");

                CollectionView.LiveSortingProperties.Add("Provider");
                CollectionView.LiveSortingProperties.Add("Name");
                CollectionView.LiveSortingProperties.Add("Categories");
                CollectionView.LiveSortingProperties.Add("Genres");
                CollectionView.LiveSortingProperties.Add("ReleaseDate");
                CollectionView.LiveSortingProperties.Add("Developers");
                CollectionView.LiveSortingProperties.Add("Tags");
                CollectionView.LiveSortingProperties.Add("Publishers");
                CollectionView.LiveSortingProperties.Add("IsInstalled");
                CollectionView.LiveSortingProperties.Add("Hidden");
                CollectionView.LiveSortingProperties.Add("Favorite");
                CollectionView.LiveSortingProperties.Add("LastActivity");
                CollectionView.LiveSortingProperties.Add("Platform");

                CollectionView.LiveFilteringProperties.Add("Provider");
                CollectionView.LiveFilteringProperties.Add("Name");
                CollectionView.LiveFilteringProperties.Add("Categories");
                CollectionView.LiveFilteringProperties.Add("Genres");
                CollectionView.LiveFilteringProperties.Add("ReleaseDate");
                CollectionView.LiveFilteringProperties.Add("Developers");
                CollectionView.LiveFilteringProperties.Add("Tags");
                CollectionView.LiveFilteringProperties.Add("Publishers");
                CollectionView.LiveFilteringProperties.Add("IsInstalled");
                CollectionView.LiveFilteringProperties.Add("Hidden");
                CollectionView.LiveFilteringProperties.Add("Favorite");
                CollectionView.LiveFilteringProperties.Add("PlatformId");

                CollectionView.IsLiveSorting = true;
                CollectionView.IsLiveFiltering = true;
                CollectionView.IsLiveGrouping = true;                   
            };
        }

        private void UpdateViewConfiguration()
        {
            logger.Debug("Updating collection view settings.");
            using (CollectionView.DeferRefresh())
            {
                CollectionView.SortDescriptions.Clear();
                CollectionView.GroupDescriptions.Clear();
                SetViewDescriptions();                
            }
        }

        public void SetViewType(GamesViewType? viewType)
        {
            if (viewType == ViewType)
            {
                return;
            }

            if (IsFullscreen)
            {
                Items.Clear();
                Items.AddRange(database.GamesCollection.FindAll().Select(x => new GameViewEntry(x, string.Empty, GetPlatformFromCache(x.PlatformId))));
            }
            else
            {
                switch (viewType)
                {
                    case GamesViewType.Standard:
                        Items.Clear();
                        Items.AddRange(database.GamesCollection.FindAll().Select(x => new GameViewEntry(x, string.Empty, GetPlatformFromCache(x.PlatformId))));
                        break;

                    case GamesViewType.CategoryGrouped:
                        Items.Clear();
                        Items.AddRange(database.GamesCollection.FindAll().SelectMany(x =>
                        {
                            if (x.Categories == null || x.Categories.Count == 0)
                            {
                                return new List<GameViewEntry>()
                                {
                            new GameViewEntry(x, null, GetPlatformFromCache(x.PlatformId))
                                };
                            }
                            else
                            {
                                return x.Categories.Select(c =>
                                {
                                    return new GameViewEntry(x, c, GetPlatformFromCache(x.PlatformId));
                                });
                            }
                        }));

                        break;
                }
            }

            this.viewType = viewType;
        }

        private Platform GetPlatformFromCache(LiteDB.ObjectId id)
        {
            return platformsCache?.FirstOrDefault(a => a.Id == id);
        }

        private void Database_PlatformUpdated(object sender, PlatformUpdatedEventArgs args)
        {
            platformsCache = database.PlatformsCollection.FindAll().ToList();
            var platformIds = args.UpdatedPlatforms.Select(a => a.NewData.Id).ToList();
            foreach (var item in Items.Where(a => a.PlatformId != null && platformIds.Contains(a.PlatformId)))
            {
                item.Platform.Platform = GetPlatformFromCache(item.PlatformId);
                item.OnPropertyChanged("Platform");
                item.OnPropertyChanged("DefaultIcon");
                item.OnPropertyChanged("DefaultImage");
            }
        }

        private void Database_PlatformsCollectionChanged(object sender, PlatformsCollectionChangedEventArgs args)
        {
            platformsCache = database.PlatformsCollection.FindAll().ToList();
        }

        private void Database_GameUpdated(object sender, GameUpdatedEventArgs args)
        {
            var replaceList = new List<Game>();
            foreach (var update in args.UpdatedGames)
            {
                if (update.OldData.Categories.IsListEqual(update.NewData.Categories))
                {
                    var existingItem = Items.FirstOrDefault(a => a.Game.Id == update.NewData.Id);
                    if (existingItem != null)
                    {
                        if (update.NewData.PlatformId != update.OldData.PlatformId)
                        {
                            existingItem.Platform = new PlatformView(update.NewData.PlatformId, GetPlatformFromCache(update.NewData.PlatformId));
                        }

                        update.NewData.CopyProperties(existingItem.Game, true);
                    }
                    else
                    {
                        logger.Warn("Receivied update for unknown game id " + update.NewData.Id);
                    }
                }
                else
                {
                    replaceList.Add(update.NewData);
                }
            }

            if (replaceList.Count > 0)
            {
                Database_GamesCollectionChanged(this, new GamesCollectionChangedEventArgs(replaceList, replaceList));
            }
        }

        private void Database_GamesCollectionChanged(object sender, GamesCollectionChangedEventArgs args)
        {
            if (args.RemovedGames.Count > 0)
            {
                var removeIds = args.RemovedGames.Select(a => a.Id);
                var toRemove = Items.Where(a => removeIds.Contains(a.Id))?.ToList();
                if (toRemove != null)
                {
                    Items.RemoveRange(toRemove);
                }
            }

            var addList = new List<GameViewEntry>();
            foreach (var game in args.AddedGames)
            {
                if (IsFullscreen)
                {
                    addList.Add(new GameViewEntry(game, string.Empty, GetPlatformFromCache(game.PlatformId)));
                }
                else
                {
                    switch (ViewType)
                    {
                        case GamesViewType.Standard:
                            addList.Add(new GameViewEntry(game, string.Empty, GetPlatformFromCache(game.PlatformId)));
                            break;

                        case GamesViewType.CategoryGrouped:
                            if (game.Categories == null || game.Categories.Count == 0)
                            {
                                addList.Add(new GameViewEntry(game, string.Empty, GetPlatformFromCache(game.PlatformId)));
                            }
                            else
                            {
                                addList.AddRange(game.Categories.Select(a => new GameViewEntry(game, a, GetPlatformFromCache(game.PlatformId))));
                            }
                            break;
                    }
                }
            }

            if (addList.Count > 0)
            {
                Items.AddRange(addList);
            }
        }
    }
}
