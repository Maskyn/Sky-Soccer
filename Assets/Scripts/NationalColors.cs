using UnityEngine;
using System.Collections;

public class NationalColors{

	private Color32 jersey;
	private Color32 pants;
	private Color32 bodyDc;
	private Color32 shoulderDc;
	private Color32 thighDc;
	private Color32 collar;

	public NationalColors(Color32 jerseyColor, Color32 pantsColor,Color32 collarColor,Color32 bodyDcColor,Color32 shoulderDcColor,Color32 thighDcColor){
		this.jersey 	= jerseyColor;
		this.pants 		= pantsColor;
		this.collar 	= collarColor;
		this.bodyDc 	= bodyDcColor;
		this.shoulderDc = shoulderDcColor;
		this.thighDc 	= thighDcColor;
	}

	public NationalColors(Color32 jerseyColor, Color32 pantsColor, Color32 collarColor, Color32 decorationsColor){
		this.jersey 	= jerseyColor;
		this.pants 		= pantsColor;
		this.collar 	= collarColor;
		this.bodyDc 	= decorationsColor;
		this.shoulderDc = decorationsColor;
		this.thighDc 	= decorationsColor;
	}

	public NationalColors(Color32 jerseyColor, Color32 pantsColor,Color32 decorationsColor){
		this.jersey 	= jerseyColor;
		this.pants 		= pantsColor;
		this.collar 	= decorationsColor;
		this.bodyDc 	= decorationsColor;
		this.shoulderDc = decorationsColor;
		this.thighDc 	= decorationsColor;
	}

	public Color32 Jersey {
		get {
			return jersey;
		}
	}
	
	public Color32 Pants {
		get {
			return pants;
		}
	}
	
	public Color32 BodyDc {
		get {
			return bodyDc;
		}
	}
	
	public Color32 ShoulderDc {
		get {
			return shoulderDc;
		}
	}
	
	public Color32 ThighDc {
		get {
			return thighDc;
		}
	}
	
	public Color32 Collar {
		get {
			return collar;
		}
	}
}
