﻿using System.Security.Permissions;

namespace HotsBpHelper.Api.Model
{
    public class HeroInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; }
    }

    public class LobbyHeroInfo
    {
        public bool IsNew { get; set; }

        public string Name { get; set; }
    }
}