using System.Collections.Generic;
using Tyme_Engine.Core;
using System;


namespace Tyme_Engine
{
    public static class AssetManager
    {
        public static List<Asset>? assets;
        static AssetManager()
        {
            assets = new List<Asset>();
        }
        public struct Asset
        {
            public string Name; // :D
            public object Asset_data;
            public Int32 Hash;
            public AssetType Type;
            public Asset(string assName, object data, int hash, AssetType type)
            {
                Name = assName;
                Asset_data = data;
                Hash = hash;
                Type = type;
            }
        }

        public enum AssetType
        {
            Texture,
            Mesh,
            Audio
        }

	//TODO: Replace GetHashCode with a different hashing function that actually delivers reliable results
        public static int RegisterAss(object Asset, string AssetName, AssetType type)
        {
            int AssetHash = Asset.GetHashCode();
            //int AssetHash
            assets.Add(new Asset(AssetName, Asset, AssetHash, type));
            return assets.Count - 1;
        }

	//TODO: Replace GetHashCode with a different hashing function that actually delivers reliable results
        public static bool assLoaded(object Asset, string AssetName)
        {
            int AssetHash = Asset.GetHashCode();
            foreach(Asset _asset in assets)
            {
                if (_asset.Hash == AssetHash)
                    return true;
            }
            return false;
        }
    }
}
