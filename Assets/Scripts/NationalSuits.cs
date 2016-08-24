using UnityEngine;
using System.Collections;

public class NationalSuits
{

		public static readonly Color32 MOUTH_COLOR = new Color32 (233, 160, 91, 255);
		public static readonly Color32 HAIR_COLOR = new Color32 (59, 21, 11, 255);

		private static readonly Color32 skin1white = new Color32 (255, 204, 153, 255);
		private static readonly Color32 skin1black = new Color32 (166, 128, 91, 255);

		private static readonly Color32 skin2white = new Color32 (252, 187, 122, 255);
		private static readonly Color32 skin2black = new Color32 (186, 147, 134, 255);

		// jerseyColor, pantsColor, collarColor, bodyDcColor, shoulderDcColor, thighDcColor
		private static readonly NationalColors ColorsGoalKeeper = new NationalColors (new Color32 (52, 73, 94, 255), new Color32 (5, 7, 8, 255), new Color32 (193, 215, 46, 255));
		//	ALGERIA,
		private static readonly NationalColors ColorsAlgeria = new NationalColors (
		new Color32 (227, 229, 241, 255), new Color32 (137, 205, 130, 255), new Color32 (137, 205, 130, 255));
		//	ARGENTINA,
		private static readonly NationalColors ColorsArgentina = new NationalColors (
		new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 255), new Color32 (29, 25, 16, 255), new Color32 (79, 148, 225, 255), new Color32 (29, 25, 16, 255), new Color32 (79, 148, 225, 255));
		//	AUSTRALIA,
		private static readonly NationalColors ColorsAustralia = new NationalColors (
		new Color32 (255, 178, 0, 255), new Color32 (36, 76, 26, 255), new Color32 (36, 76, 26, 255));
		//	BELGIUM,
		private static readonly NationalColors ColorsBelgium = new NationalColors (
		new Color32 (232, 0, 0, 255), new Color32 (214, 0, 0, 255), new Color32 (239, 215, 63, 255));
		//	BOSNIA_AND_HERZEGOVINA,
		private static readonly NationalColors ColorsBosniaAndHerzegovina = new NationalColors (
		new Color32 (218, 218, 218, 255), new Color32 (197, 197, 197, 255), new Color32 (38, 83, 138, 255));
		//	BRAZIL,
		private static readonly NationalColors ColorsBrasil = new NationalColors (
		new Color32 (249, 196, 2, 255), new Color32 (3, 38, 142, 255), new Color32 (1, 136, 59, 255));
		//	CAMEROON,
		private static readonly NationalColors ColorsCameroon = new NationalColors (
		new Color32 (5, 113, 89, 255), new Color32 (196, 16, 43, 255), new Color32 (196, 16, 43, 255));
		//	CHILE,
		private static readonly NationalColors ColorsChile = new NationalColors (
		new Color32 (254, 0, 0, 255), new Color32 (5, 51, 128, 255), new Color32 (208, 228, 227, 255));
		//	CHINA
		private static readonly NationalColors ColorsChina = new NationalColors (
		new Color32 (254, 0, 0, 255), new Color32 (218, 1, 20, 255), new Color32 (231, 182, 135, 255));
		//	COLOMBIA,
		private static readonly NationalColors ColorsColombia = new NationalColors (
		new Color32 (253, 209, 22, 255), new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 255), new Color32 (32, 56, 120, 255));
		//	COSTA_RICA,
		private static readonly NationalColors ColorsCostaRica = new NationalColors (
		new Color32 (206, 16, 41, 255), new Color32 (0, 41, 107, 255), new Color32 (206, 16, 41, 255));
		//	COTE_D_IVOIRE,
		private static readonly NationalColors ColorsCoteDIvoire = new NationalColors (
		new Color32 (227, 116, 0, 255), new Color32 (241, 129, 0, 255), new Color32 (255, 255, 255, 255));
		//	CROATIA,
		private static readonly NationalColors ColorsCroazia = new NationalColors (
		new Color32 (208, 16, 37, 255), new Color32 (255, 255, 255, 255), new Color32 (208, 16, 37, 255), new Color32 (255, 255, 255, 255), new Color32 (208, 16, 37, 255), new Color32 (208, 16, 37, 255));
		//	FRANCE,
		private static readonly NationalColors ColorsFrance = new NationalColors (
		new Color32 (41, 64, 106, 255), new Color32 (219, 219, 219, 255), new Color32 (219, 219, 219, 255));
		//	ECUADOR,
		private static readonly NationalColors ColorsEcuador = new NationalColors (
		new Color32 (207, 177, 29, 255), new Color32 (24, 30, 118, 255), new Color32 (24, 30, 118, 255));
		//	ENGLAND,
		private static readonly NationalColors ColorsEngland = new NationalColors (
		new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 255));
		//	GERMANY,
		private static readonly NationalColors ColorsGermany = new NationalColors (
		new Color32 (218, 218, 218, 255), new Color32 (197, 197, 197, 255), new Color32 (0, 0, 0, 255));
		//	GHANA,
		private static readonly NationalColors ColorsGhana = new NationalColors (
		new Color32 (218, 218, 218, 255), new Color32 (197, 197, 197, 255), new Color32 (248, 236, 76, 255));
		//	GREECE,
		private static readonly NationalColors ColorsGreece = new NationalColors (
		new Color32 (34, 68, 192, 255), new Color32 (34, 68, 192, 255), new Color32 (197, 197, 197, 255));
		//	HONDURAS,
		private static readonly NationalColors ColorsHonduras = new NationalColors (
		new Color32 (218, 218, 218, 255), new Color32 (197, 197, 197, 255), new Color32 (14, 14, 134, 255));
		//	IRAN,
		private static readonly NationalColors ColorsIran = new NationalColors (
		new Color32 (216, 9, 29, 255), new Color32 (227, 47, 43, 255), new Color32 (56, 115, 86, 255));
		//	ITALY,
		private static readonly NationalColors ColorsItaly = new NationalColors (
		new Color32 (0, 127, 255, 255), new Color32 (255, 255, 255, 255), new Color32 (0, 127, 255, 255), new Color32 (255, 255, 255, 255));
		//	JAPAN,
		private static readonly NationalColors ColorsJapan = new NationalColors (
		new Color32 (34, 65, 147, 255), new Color32 (17, 18, 82, 255), new Color32 (34, 65, 147, 255), new Color32 (255, 255, 255, 255));
		//	KOREA_REPUBLIC,
		private static readonly NationalColors ColorsKoreaRepublic = new NationalColors (
		new Color32 (234, 3, 39, 255), new Color32 (18, 44, 167, 255), new Color32 (216, 5, 38, 255));
		//	MEXICO,
		private static readonly NationalColors ColorsMexico = new NationalColors (
		new Color32 (6, 101, 35, 255), new Color32 (194, 198, 197, 255), new Color32 (6, 101, 35, 255), new Color32 (216, 225, 220, 255));
		//	MOLDOVA,
		private static readonly NationalColors ColorsMoldova = new NationalColors (
		new Color32 (58, 69, 193, 255), new Color32 (32, 38, 132, 255), new Color32 (239, 239, 239, 255));
		//	NETHERLANDS,
		private static readonly NationalColors ColorsNetherlands = new NationalColors (
		new Color32 (254, 102, 3, 255), new Color32 (255, 105, 5, 255), new Color32 (0, 0, 0, 255));
		//	NIGERIA,
		private static readonly NationalColors ColorsNigeria = new NationalColors (
		new Color32 (32, 123, 92, 255), new Color32 (24, 106, 84, 255), new Color32 (228, 228, 228, 255));
		//	PORTUGAL,
		private static readonly NationalColors ColorsPortugal = new NationalColors (
		new Color32 (119, 16, 37, 255), new Color32 (111, 16, 38, 255), new Color32 (82, 109, 56, 255), new Color32 (92, 19, 36, 255));
		//	RUSSIA,
		private static readonly NationalColors ColorsRussia = new NationalColors (
		new Color32 (88, 31, 48, 255), new Color32 (100, 25, 48, 255), new Color32 (0, 0, 0, 255), new Color32 (236, 219, 150, 255));
		//	SPAIN,
		private static readonly NationalColors ColorsSpain = new NationalColors (
		new Color32 (254, 0, 0, 255), new Color32 (218, 1, 20, 255), new Color32 (218, 1, 20, 255), new Color32 (218, 1, 20, 255), new Color32 (238, 228, 218, 255), new Color32 (238, 228, 218, 255));
		//	SWEDEN,
		private static readonly NationalColors ColorsSweden = new NationalColors (
		new Color32 (252, 225, 84, 255), new Color32 (2, 88, 197, 255), new Color32 (0, 111, 209, 255));
		//	SWITERLAND,
		private static readonly NationalColors ColorsSwiterland = new NationalColors (
		new Color32 (218, 0, 5, 255), new Color32 (204, 0, 11, 255), new Color32 (218, 0, 5, 255), new Color32 (216, 216, 216, 255));
		//	TURKEY,
		private static readonly NationalColors ColorsTurkey = new NationalColors (
		new Color32 (233, 33, 59, 255), new Color32 (219, 22, 50, 255), new Color32 (233, 223, 248, 255));
		//	USA,
		private static readonly NationalColors ColorsUsa = new NationalColors (
		new Color32 (218, 218, 218, 255), new Color32 (197, 197, 197, 255), new Color32 (155, 27, 28, 255));
		//	URUGUAY,
		private static readonly NationalColors ColorsUruguay = new NationalColors (
		new Color32 (76, 132, 179, 255), new Color32 (0, 0, 0, 255), new Color32 (203, 203, 203, 255));

		// get suit of the nation
		public static NationalSuit getSuitForNation (Nationals nation)
		{
				// jerseyColor, pantsColor, bodyDcColor, shoulderDcColor, thighDcColor, collarColor
				switch (nation) {
				case Nationals.ALGERIA:
						return new NationalSuit (nation, ColorsAlgeria);
				case Nationals.ARGENTINA:
						return new NationalSuit (nation, ColorsArgentina);
				case Nationals.AUSTRALIA:
						return new NationalSuit (nation, ColorsAustralia);
				case Nationals.BELGIUM:
						return new NationalSuit (nation, ColorsBelgium);
				case Nationals.BOSNIA_AND_HERZEGOVINA:
						return new NationalSuit (nation, ColorsBosniaAndHerzegovina);
				case Nationals.BRAZIL:
						return new NationalSuit (nation, ColorsBrasil);
				case Nationals.CAMEROON:
						return new NationalSuit (nation, ColorsCameroon);
				case Nationals.CHILE:
						return new NationalSuit (nation, ColorsChile);
				case Nationals.CHINA:
						return new NationalSuit (nation, ColorsChina);
				case Nationals.COLOMBIA:
						return new NationalSuit (nation, ColorsColombia);
				case Nationals.COSTA_RICA:
						return new NationalSuit (nation, ColorsCostaRica);
				case Nationals.COTE_D_IVOIRE:
						return new NationalSuit (nation, ColorsCoteDIvoire);
				case Nationals.CROATIA:
						return new NationalSuit (nation, ColorsCroazia);
				case Nationals.ECUADOR:
						return new NationalSuit (nation, ColorsEcuador);
				case Nationals.ENGLAND:
						return new NationalSuit (nation, ColorsEngland);
				case Nationals.FRANCE:
						return new NationalSuit (nation, ColorsFrance);
				case Nationals.GERMANY:
						return new NationalSuit (nation, ColorsGermany);
				case Nationals.GHANA:
						return new NationalSuit (nation, ColorsGhana);
				case Nationals.GREECE:
						return new NationalSuit (nation, ColorsGreece);
				case Nationals.HONDURAS:
						return new NationalSuit (nation, ColorsHonduras);
				case Nationals.IRAN:
						return new NationalSuit (nation, ColorsIran);
				case Nationals.ITALY:
						return new NationalSuit (nation, ColorsItaly);
				case Nationals.JAPAN:
						return new NationalSuit (nation, ColorsJapan);
				case Nationals.KOREA_REPUBLIC:
						return new NationalSuit (nation, ColorsKoreaRepublic);
				case Nationals.MEXICO:
						return new NationalSuit (nation, ColorsMexico);
				case Nationals.MOLDOVA:
						return new NationalSuit (nation, ColorsMoldova);
				case Nationals.NETHERLANDS:
						return new NationalSuit (nation, ColorsNetherlands);
				case Nationals.NIGERIA:
						return new NationalSuit (nation, ColorsNigeria);
				case Nationals.PORTUGAL:
						return new NationalSuit (nation, ColorsPortugal);
				case Nationals.RUSSIA:
						return new NationalSuit (nation, ColorsRussia);
				case Nationals.SPAIN:
						return new NationalSuit (nation, ColorsSpain);
				case Nationals.SWEDEN:
						return new NationalSuit (nation, ColorsSweden);
				case Nationals.SWITERLAND:
						return new NationalSuit (nation, ColorsSwiterland);
				case Nationals.TURKEY:
						return new NationalSuit (nation, ColorsTurkey);
				case Nationals.URUGUAY:
						return new NationalSuit (nation, ColorsUruguay);
				case Nationals.USA:
						return new NationalSuit (nation, ColorsUsa);
				default:
						return new NationalSuit (nation, ColorsGoalKeeper);
				}
		}

		public static int getNationId (Nationals nation)
		{
				// jerseyColor, pantsColor, bodyDcColor, shoulderDcColor, thighDcColor, collarColor
				switch (nation) {
				case Nationals.ALGERIA:
						return 0;
				case Nationals.ARGENTINA:
						return 1;
				case Nationals.AUSTRALIA:
						return 2;
				case Nationals.BELGIUM:
						return 3;
				case Nationals.BOSNIA_AND_HERZEGOVINA:
						return 4;
				case Nationals.BRAZIL:
						return 5;
				case Nationals.CAMEROON:
						return 6;
				case Nationals.CHILE:
						return 7;
				case Nationals.CHINA:
						return 8;
				case Nationals.COLOMBIA:
						return 9;
				case Nationals.COSTA_RICA:
						return 10;
				case Nationals.COTE_D_IVOIRE:
						return 11;
				case Nationals.CROATIA:
						return 12;
				case Nationals.ECUADOR:
						return 13;
				case Nationals.ENGLAND:
						return 14;
				case Nationals.FRANCE:
						return 15;
				case Nationals.GERMANY:
						return 16;
				case Nationals.GHANA:
						return 17;
				case Nationals.GREECE:
						return 18;
				case Nationals.HONDURAS:
						return 19;
				case Nationals.IRAN:
						return 20;
				case Nationals.ITALY:
						return 21;
				case Nationals.JAPAN:
						return 22;
				case Nationals.KOREA_REPUBLIC:
						return 23;
				case Nationals.MEXICO:
						return 24;
				case Nationals.MOLDOVA:
						return 25;
				case Nationals.NETHERLANDS:
						return 26;
				case Nationals.NIGERIA:
						return 27;
				case Nationals.PORTUGAL:
						return 28;
				case Nationals.RUSSIA:
						return 29;
				case Nationals.SPAIN:
						return 30;
				case Nationals.SWEDEN:
						return 31;
				case Nationals.SWITERLAND:
						return 32;
				case Nationals.TURKEY:
						return 33;
				case Nationals.URUGUAY:
						return 34;
				case Nationals.USA:
						return 35;
				default:
						return 40;
				}
		}


		public static Nationals getNationById (byte id)
		{
				switch (id) {
				case 0:
						return Nationals.ALGERIA;
				case 1:
						return Nationals.ARGENTINA;
				case 2:
						return Nationals.AUSTRALIA;
				case 3:
						return Nationals.BELGIUM;
				case 4:
						return Nationals.BOSNIA_AND_HERZEGOVINA;
				case 5:
						return Nationals.BRAZIL;
				case 6:
						return Nationals.CAMEROON;
				case 7:
						return Nationals.CHILE;
				case 8:
						return Nationals.CHINA;
				case 9:
						return Nationals.COLOMBIA;
				case 10:
						return Nationals.COSTA_RICA;
				case 11:
						return Nationals.COTE_D_IVOIRE;
				case 12:
						return Nationals.CROATIA;
				case 13:
						return Nationals.ECUADOR;
				case 14:
						return Nationals.ENGLAND;
				case 15:
						return Nationals.FRANCE;
				case 16:
						return Nationals.GERMANY;
				case 17:
						return Nationals.GHANA;
				case 18:
						return Nationals.GREECE;
				case 19:
						return Nationals.HONDURAS;
				case 20:
						return Nationals.IRAN;
				case 21:
						return Nationals.ITALY;
				case 22:
						return Nationals.JAPAN;
				case 23:
						return Nationals.KOREA_REPUBLIC;
				case 24:
						return Nationals.MEXICO;
				case 25:
						return Nationals.MOLDOVA;
				case 26:
						return Nationals.NETHERLANDS;
				case 27:
						return Nationals.NIGERIA;
				case 28:
						return Nationals.PORTUGAL;
				case 29:
						return Nationals.RUSSIA;
				case 30:
						return Nationals.SPAIN;
				case 31:
						return Nationals.SWEDEN;
				case 32:
						return Nationals.SWITERLAND;
				case 33:
						return Nationals.TURKEY;
				case 34:
						return Nationals.URUGUAY;
				case 35:
						return Nationals.USA;
				default:
						return Nationals.NONE;
				}
		}

		public static NationalSuit getGoalKeeperSuit ()
		{
				return new NationalSuit (Nationals.NONE, ColorsGoalKeeper);
		}

		public static string getNationNameShort (Nationals nation)
		{
				switch (nation) {
				case Nationals.ALGERIA:
						return "ALG";
				case Nationals.ARGENTINA:
						return "ARG";
				case Nationals.AUSTRALIA:
						return "AUS";
				case Nationals.BELGIUM:
						return "BEL";
				case Nationals.BOSNIA_AND_HERZEGOVINA:
						return "BIH";
				case Nationals.BRAZIL:
						return "BRA";
				case Nationals.CAMEROON:
						return "CMR";
				case Nationals.CHILE:
						return "CHI";
				case Nationals.CHINA:
						return "CHN";
				case Nationals.COLOMBIA:
						return "COL";
				case Nationals.COSTA_RICA:
						return "CRC";
				case Nationals.COTE_D_IVOIRE:
						return "CIV";
				case Nationals.CROATIA:
						return "CRO";
				case Nationals.ECUADOR:
						return "ECU";
				case Nationals.ENGLAND:
						return "ENG";
				case Nationals.FRANCE:
						return "FRA";
				case Nationals.GERMANY:
						return "GER";
				case Nationals.GHANA:
						return "GHA";
				case Nationals.GREECE:
						return "GRE";
				case Nationals.HONDURAS:
						return "HON";
				case Nationals.IRAN:
						return "IRN";
				case Nationals.ITALY:
						return "ITA";
				case Nationals.JAPAN:
						return "JPN";
				case Nationals.KOREA_REPUBLIC:
						return "KOR";
				case Nationals.MEXICO:
						return "MEX";
				case Nationals.MOLDOVA:
						return "MDA";
				case Nationals.NETHERLANDS:
						return "NED";
				case Nationals.NIGERIA:
						return "NGA";
				case Nationals.PORTUGAL:
						return "POR";
				case Nationals.RUSSIA:
						return "RUS";
				case Nationals.SPAIN:
						return "ESP";
				case Nationals.SWEDEN:
						return "SWE";
				case Nationals.SWITERLAND:
						return "SUI";
				case Nationals.TURKEY:
						return "TUR";
				case Nationals.URUGUAY:
						return "URU";
				case Nationals.USA:
						return "USA";
				default:
						return "ERRRROr"; // should never happen
				}
		}

		public static string getNationNameLong (Nationals nation)
		{
				switch (nation) {
				case Nationals.ALGERIA:
						return Localization.Get ("ALGERIA");
				case Nationals.ARGENTINA:
						return Localization.Get ("ARGENTINA");
				case Nationals.AUSTRALIA:
						return Localization.Get ("AUSTRALIA");
				case Nationals.BELGIUM:
						return Localization.Get ("BELGIUM");
				case Nationals.BOSNIA_AND_HERZEGOVINA:
						return Localization.Get ("BOSNIA");
				case Nationals.BRAZIL:
						return Localization.Get ("BRAZIL");
				case Nationals.CAMEROON:
						return Localization.Get ("CAMEROON");
				case Nationals.CHILE:
						return Localization.Get ("CHILE");
				case Nationals.CHINA:
						return Localization.Get ("CHINA");
				case Nationals.COLOMBIA:
						return Localization.Get ("COLOMBIA");
				case Nationals.COSTA_RICA:
						return Localization.Get ("COSTA RICA");
				case Nationals.COTE_D_IVOIRE:
						return Localization.Get ("COTEDIVOIRE");
				case Nationals.CROATIA:
						return Localization.Get ("CROATIA");
				case Nationals.ECUADOR:
						return Localization.Get ("ECUADOR");
				case Nationals.ENGLAND:
						return Localization.Get ("ENGLAND");
				case Nationals.FRANCE:
						return Localization.Get ("FRANCE");
				case Nationals.GERMANY:
						return Localization.Get ("GERMANY");
				case Nationals.GHANA:
						return Localization.Get ("GHANA");
				case Nationals.GREECE:
						return Localization.Get ("GREECE");
				case Nationals.HONDURAS:
						return Localization.Get ("HONDURAS");
				case Nationals.IRAN:
						return Localization.Get ("IRAN");
				case Nationals.ITALY:
						return Localization.Get ("ITALY");
				case Nationals.JAPAN:
						return Localization.Get ("JAPAN");
				case Nationals.KOREA_REPUBLIC:
						return Localization.Get ("KOREA REPUBLIC");
				case Nationals.MEXICO:
						return Localization.Get ("MEXICO");
				case Nationals.MOLDOVA:
						return Localization.Get ("MOLDOVA");
				case Nationals.NETHERLANDS:
						return Localization.Get ("NETHERLANDS");
				case Nationals.NIGERIA:
						return Localization.Get ("NIGERIA");
				case Nationals.PORTUGAL:
						return Localization.Get ("PORTUGAL");
				case Nationals.RUSSIA:
						return Localization.Get ("RUSSIA");
				case Nationals.SPAIN:
						return Localization.Get ("SPAIN");
				case Nationals.SWEDEN:
						return Localization.Get ("SWEDEN");
				case Nationals.SWITERLAND:
						return Localization.Get ("SWITERLAND");
				case Nationals.TURKEY:
						return Localization.Get ("TURKEY");
				case Nationals.URUGUAY:
						return Localization.Get ("URUGUAY");
				case Nationals.USA:
						return Localization.Get ("USA");
				default:
						return "ERRRROr"; // should never happen
				}
		}

		public static Sprite getNationFlag (Nationals nation)
		{
				switch (nation) {
				case Nationals.ALGERIA:
						return Flags._ALGERIA;
				case Nationals.ARGENTINA:
						return Flags._ARGENTINA;
				case Nationals.AUSTRALIA:
						return Flags._AUSTRALIA;
				case Nationals.BELGIUM:
						return Flags._BELGIUM;
				case Nationals.BOSNIA_AND_HERZEGOVINA:
						return Flags._BOSNIA_AND_HERZEGOVINA;
				case Nationals.BRAZIL:
						return Flags._BRAZIL;
				case Nationals.CAMEROON:
						return Flags._CAMEROON;
				case Nationals.CHILE:
						return Flags._CHILE;
				case Nationals.CHINA:
						return Flags._CHINA;
				case Nationals.COLOMBIA:
						return Flags._COLOMBIA;
				case Nationals.COSTA_RICA:
						return Flags._COSTA_RICA;
				case Nationals.COTE_D_IVOIRE:
						return Flags._COTE_D_IVOIRE;
				case Nationals.CROATIA:
						return Flags._CROATIA;
				case Nationals.ECUADOR:
						return Flags._ECUADOR;
				case Nationals.ENGLAND:
						return Flags._ENGLAND;
				case Nationals.FRANCE:
						return Flags._FRANCE;
				case Nationals.GERMANY:
						return Flags._GERMANY;
				case Nationals.GHANA:
						return Flags._GHANA;
				case Nationals.GREECE:
						return Flags._GREECE;
				case Nationals.HONDURAS:
						return Flags._HONDURAS;
				case Nationals.IRAN:
						return Flags._IRAN;
				case Nationals.ITALY:
						return Flags._ITALY;
				case Nationals.JAPAN:
						return Flags._JAPAN;
				case Nationals.KOREA_REPUBLIC:
						return Flags._KOREA_REPUBLIC;
				case Nationals.MEXICO:
						return Flags._MEXICO;
				case Nationals.MOLDOVA:
						return Flags._MOLDOVA;
				case Nationals.NETHERLANDS:
						return Flags._NETHERLAND;
				case Nationals.NIGERIA:
						return Flags._NIGERIA;
				case Nationals.PORTUGAL:
						return Flags._PORTUGAL;
				case Nationals.RUSSIA:
						return Flags._RUSSIA;
				case Nationals.SPAIN:
						return Flags._SPAIN;
				case Nationals.SWEDEN:
						return Flags._SWEDEN;
				case Nationals.SWITERLAND:
						return Flags._SWITERLAND;
				case Nationals.TURKEY:
						return Flags._TURKEY;
				case Nationals.URUGUAY:
						return Flags._URUGUAY;
				case Nationals.USA:
						return Flags._USA;
				default:
						return null; // should never happen
				}
		}

		// returns the decoration id. It's a number and 0 means no decoration.
		// body, shoulder, thigh
		public static byte[] getDecorations (Nationals nation)
		{
				switch (nation) {
				case Nationals.ALGERIA:
						return new byte[] {0,3,0}; // done
				case Nationals.ARGENTINA:
						return new byte[] {2,5,0}; // done
				case Nationals.AUSTRALIA:
						return new byte[] {0,3,0}; // done
				case Nationals.BELGIUM:
						return new byte[] {0,0,0}; // done
				case Nationals.BOSNIA_AND_HERZEGOVINA:
						return new byte[] {0,5,0}; // done 
				case Nationals.BRAZIL:
						return new byte[] {0,3,0}; // done 
				case Nationals.CAMEROON:
						return new byte[] {0,0,0}; // done 
				case Nationals.CHILE:
						return new byte[] {0,4,0}; // done
				case Nationals.COLOMBIA:
						return new byte[] {0,5,0}; // done
				case Nationals.COSTA_RICA:
						return new byte[] {0,3,0}; // done
				case Nationals.COTE_D_IVOIRE:
						return new byte[] {0,0,0}; // done
				case Nationals.CROATIA:
						return new byte[] {1,0,0}; // done
				case Nationals.ECUADOR:
						return new byte[] {0,0,0}; // done
				case Nationals.ENGLAND:
						return new byte[] {0,0,0}; // done
				case Nationals.FRANCE:
						return new byte[] {0,0,0}; // done
				case Nationals.GERMANY:
						return new byte[] {5,5,0}; // done
				case Nationals.GHANA:
						return new byte[] {0,0,0}; // done
				case Nationals.GREECE:
						return new byte[] {0,1,0}; // done
				case Nationals.HONDURAS:
						return new byte[] {0,5,0}; // done
				case Nationals.IRAN:
						return new byte[] {0,0,0}; // done
				case Nationals.ITALY:
						return new byte[] {0,4,0}; // done
				case Nationals.JAPAN:
						return new byte[] {0,5,0}; // done
				case Nationals.KOREA_REPUBLIC:
						return new byte[] {0,0,0}; // done
				case Nationals.MEXICO:
						return new byte[] {0,5,0}; // done 
				case Nationals.NETHERLANDS:
						return new byte[] {0,0,0}; // done
				case Nationals.NIGERIA:
						return new byte[] {0,3,0}; // done
				case Nationals.PORTUGAL:
						return new byte[] {4,0,0}; // done
				case Nationals.RUSSIA:
						return new byte[] {0,5,0}; // done 
				case Nationals.SPAIN:
						return new byte[] {3,5,0}; // done
				case Nationals.SWEDEN:
						return new byte[] {0,5,0}; // done
				case Nationals.SWITERLAND:
						return new byte[] {0,4,0}; // done
				case Nationals.TURKEY:
						return new byte[] {0,0,0}; // done
				case Nationals.URUGUAY:
						return new byte[] {0,4,0}; // done
				case Nationals.USA:
						return new byte[] {0,0,0}; // done
				default:
						return new byte[] {0,0,0}; // should never happen
				}
		}

		// get the skin color depending on the nation
		public static Color getSkinColor (Nationals nation, bool firstSkin)
		{
				Color skinWhite, skinBlack;
				if (firstSkin) {
						skinWhite = skin1white;
						skinBlack = skin1black;
				} else {
						skinWhite = skin2white;
						skinBlack = skin2black;
				}

				switch (nation) {
				// only black
				case Nationals.ALGERIA:
				case Nationals.BRAZIL:
				case Nationals.CAMEROON:
				case Nationals.CHILE:
				case Nationals.COLOMBIA:
				case Nationals.COTE_D_IVOIRE:
				case Nationals.ECUADOR:
				case Nationals.GHANA:
				case Nationals.HONDURAS:
				case Nationals.IRAN:
				case Nationals.MEXICO:
				case Nationals.NIGERIA:
				case Nationals.URUGUAY:
						return skinBlack;
				default:
						return skinWhite;
				}
		}
}
