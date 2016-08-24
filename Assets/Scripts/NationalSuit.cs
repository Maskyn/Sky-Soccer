using UnityEngine;
using System.Collections;

public class NationalSuit{

	// HEAD
	public const string NM_HAIR = "hair";
	public const string NM_NOSE = "nose";
	public const string NM_MOUTH = "mouth";
	public const string NM_EYE = "eye";
	public const string NM_EAR = "ear";
	public const string NM_EYEBROWS = "eyebrows";
	public const string NM_HEAD = "head";
	public const string NM_NECK = "neck";

	public const string NM_HAND = "hand";
	// KNEE ginocchio
	public const string NM_KNEE_UP_1 = "knee_up_1";
	public const string NM_KNEE_MIDDLE_1 = "knee_middle_1";
	public const string NM_KNEE_DOWN_1 = "knee_down_1";

	public const string NM_KNEE_UP_2 = "knee_up_2";
	public const string NM_KNEE_MIDDLE_2 = "knee_middle_2";
	public const string NM_KNEE_DOWN_2 = "knee_down_2";

	// SHOE scarpa
	public const string NM_SHOE_1 = "shoe_1";
	public const string NM_SHOE_2 = "shoe_2";

	// ELBOW gomito
	public const string NM_ELBOW_UP = "elbow_up";
	public const string NM_ELBOW_MIDDLE = "elbow_middle";
	public const string NM_ELBOW_DOWN = "elbow_down";

	// THIGH coscia
	public const string NM_THIGH_UP = "thigh_up";
	public const string NM_THIGH_DOWN_1 = "thigh_down_1";
	public const string NM_THIGH_DOWN_2 = "thigh_down_2";
	// decorations
	public const string NM_THIGH_DC_1_1 = "thigh_dc_1_1";
	public const string NM_THIGH_DC_1_2 = "thigh_dc_1_2";
	// BODY
	public const string NM_BODY = "body";
	public const string NM_COLLAR = "collar";
	public const string NM_BODY_DC_1 = "body_dc_1";
	public const string NM_BODY_DC_2 = "body_dc_2";
	public const string NM_BODY_DC_3 = "body_dc_3";
	public const string NM_BODY_DC_4 = "body_dc_4";
	public const string NM_BODY_DC_5 = "body_dc_5";

	public const string NM_SHOULDER = "shoulder";
	public const string NM_SHOULDER_DC_1 = "shoulder_dc_1";
	public const string NM_SHOULDER_DC_2 = "shoulder_dc_2";
	public const string NM_SHOULDER_DC_3 = "shoulder_dc_3";
	public const string NM_SHOULDER_DC_4 = "shoulder_dc_4";
	public const string NM_SHOULDER_DC_5 = "shoulder_dc_5";

	private Nationals nation;
	private NationalColors nationalColors;

	public NationalSuit(Nationals nation, NationalColors nationalColors){
		this.nation = nation;
		this.nationalColors = nationalColors;
	}

	public Nationals Nation {
		get {
			return nation;
		}
	}

	public NationalColors NationalColors {
		get {
			return nationalColors;
		}
	}
}
