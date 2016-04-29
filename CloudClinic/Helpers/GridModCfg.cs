namespace CloudClinic.Helpers
{
    /// <summary>
    /// Grid mods configuration
    /// </summary>
    public class GridModCfg
    {
        private readonly GridModInfo info = new GridModInfo();

        /// <summary>
        /// automatically go to next/prev page when scrolling to the end/beginning of the page
        /// </summary>
        /// <returns></returns>
        public GridModCfg InfiniteScroll()
        {
            info.InfiniteScroll = true;
            return this;
        }

        /// <summary>
        /// add page info ( page 1 of 75 ) in the right bottom corner of the grid
        /// </summary>
        /// <returns></returns>
        public GridModCfg PageInfo()
        {
            info.PageInfo = true;
            return this;
        }

        /// <summary>
        /// automatically switch the pager to a more compact version on smaller screens, or when resizing the browser to smaller size
        /// </summary>
        /// <returns></returns>
        public GridModCfg AutoMiniPager()
        {
            info.AutoMiniPager = true;
            return this;
        }

        /// <summary>
        /// add page size selector 
        /// </summary>
        /// <returns></returns>
        public GridModCfg PageSize()
        {
            info.PageSize = true;
            return this;
        }

        /// <summary>
        /// add columns selector dropdown
        /// </summary>
        /// <returns></returns>
        public GridModCfg ColumnsSelector()
        {
            info.ColumnsSelector = true;
            return this;
        }

        /// <summary>
        /// Autohide columns
        /// </summary>
        /// <returns></returns>
        public GridModCfg ColumnsAutohide()
        {
            info.ColumnsAutohide = true;
            return this;
        }

        /// <summary>
        /// set Inline editing urls
        /// </summary>
        /// <param name="createUrl"></param>
        /// <param name="editUrl"></param>
        /// <returns></returns>
        public GridModCfg InlineEdit(string createUrl, string editUrl)
        {
            info.InlineEdit = true;
            info.CreateUrl = createUrl;
            info.EditUrl = editUrl;
            return this;
        }

        internal GridModInfo GetInfo()
        {
            return info;
        }
    }
}