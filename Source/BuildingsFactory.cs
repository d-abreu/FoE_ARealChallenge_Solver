namespace ForgeOfEmpiresARealChallengeSolver
{
    static class BuildingsFactory
    {
        public static Buildings GetAllBuildings()
        {
            var buildings = new Buildings();
            buildings.Add("Hut", 14);
            buildings.Add("Stilt House", 22);
            buildings.Add("Chalet", 32);
            buildings.Add("Thatched House", 27);
            buildings.Add("Villa", 87, true);
            buildings.Add("Roof Tile House", 44);
            buildings.Add("Cottage", 73);
            buildings.Add("Frame House", 67);
            buildings.Add("Multistory House", 89, true);
            buildings.Add("Clapboard House", 111);
            buildings.Add("Mansion", 188, true);
            buildings.Add("Brownstone House", 94);
            buildings.Add("Town House", 156);
            buildings.Add("Manor", 246, true);
            buildings.Add("Estate House", 123);
            buildings.Add("Apartment House", 205);
            buildings.Add("Plantation House", 311, true);
            buildings.Add("Arcade House", 155);
            buildings.Add("Country House", 207);
            buildings.Add("Gambrel Roof House", 259);
            buildings.Add("Urban Residence", 569, true);
            buildings.Add("Workers House", 285);
            buildings.Add("Boarding House", 380);
            buildings.Add("Victorian House", 474);
            return buildings;
        }

        public static Buildings GetAllNonPremiumBuildings()
        {
            var buildings = GetAllBuildings();
            buildings.RemoveAll(t => t.IsPremium);
            return buildings;
        }
    }
}