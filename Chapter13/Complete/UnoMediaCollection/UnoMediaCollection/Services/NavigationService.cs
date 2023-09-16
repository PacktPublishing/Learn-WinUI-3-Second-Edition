using UnoMediaCollection.Interfaces;
using System.Collections.Concurrent;

namespace UnoMediaCollection.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService(Frame rootFrame)
        {
            AppFrame = rootFrame;
        }

        private readonly IDictionary<string, Type> _pages = new ConcurrentDictionary<string, Type>();

        public const string RootPage = "(Root)";

        public const string UnknownPage = "(Unknown)";

        internal static Frame AppFrame;

        public void Configure(string page, Type type)
        {
            if (_pages.Values.Any(v => v == type))
            {
                throw new ArgumentException($"The {type.Name} view has already been registered under another name.");
            }

            _pages[page] = type;
        }

        /// <summary>
        /// Gets the name of the currently displayed page.
        /// </summary>
        public string CurrentPage
        {
            get
            {
                var frame = AppFrame;

                if (frame.BackStackDepth == 0)
                    return RootPage;

                if (frame.Content == null)
                    return UnknownPage;

                var type = frame.Content.GetType();

                if (_pages.Values.All(v => v != type))
                    return UnknownPage;

                var item = _pages.Single(i => i.Value == type);

                return item.Key;
            }
        }

        public void NavigateTo(string page)
        {
            NavigateTo(page, null);
        }

        public void NavigateTo(string page, object parameter)
        {
            if (!_pages.ContainsKey(page))
            {
                throw new ArgumentException($"Unable to find a page registered with the name {page}.");
            }

            AppFrame.Navigate(_pages[page], parameter);
        }

        public void GoBack()
        {
            if (AppFrame?.CanGoBack == true)
            {
                AppFrame.GoBack();
            }
        }
    }
}