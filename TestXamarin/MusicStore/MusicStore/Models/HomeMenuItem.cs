﻿namespace MusicStore.Models
{
    public enum MenuItemType
    {
        Login,
        Registro,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
