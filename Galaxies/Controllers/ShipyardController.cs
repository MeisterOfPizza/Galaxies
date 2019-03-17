using Galaxies.Datas;
using Galaxies.Datas.Player;
using Galaxies.Progression;

namespace Galaxies.Controllers
{

    static class ShipyardController
    {
        
        public static PlayerShipTemplate[] PlayerShipTemplates       { get; set; }
        public static PlayerShipTemplate   CurrentPlayerShipTemplate { get; set; }

        public static void Initialize()
        {
            CreatePlayerShipTemplates();
        }

        private static void CreatePlayerShipTemplates()
        {
            var playerShipTemplateDatas = DataController.LoadAllData<PlayerShipTemplateData>(DataFileType.PlayerShipTemplates, "PlayerShipTemplate");
            PlayerShipTemplates = new PlayerShipTemplate[playerShipTemplateDatas.Length];

            for (int i = 0; i < playerShipTemplateDatas.Length; i++)
            {
                PlayerShipTemplates[i] = new PlayerShipTemplate(playerShipTemplateDatas[i], i == 0 /* Always unlock the first one */);
            }
        }

        public static void RefillShipStats()
        {
            CurrentPlayerShipTemplate.Ship.RefillStats();
        }

        public static void AssignPlayerShip(PlayerShipTemplate playerShipTemplate)
        {
            playerShipTemplate.Unlocked = true;

            CurrentPlayerShipTemplate = playerShipTemplate;

            PlayerController.AssignNewShip(playerShipTemplate.Ship);
        }

    }

}
