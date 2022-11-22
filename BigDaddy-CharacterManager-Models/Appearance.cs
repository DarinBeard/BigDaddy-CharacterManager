using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDaddy_CharacterManager_Models
{
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class CharacterAppearance
	{
		public Appearance appearance { get; set; }
	}

	public class Appearance
	{
		public List<Component> components { get; set; }
		public int eyeColor { get; set; }
		public FaceFeatures faceFeatures { get; set; }
		public Hair hair { get; set; }
		public HeadBlend headBlend { get; set; }
		public HeadOverlays headOverlays { get; set; }
		public string model { get; set; }
		public List<Prop> props { get; set; }
		public Tattoos tattoos { get; set; }
	}


	public class Ageing
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Beard
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Blemishes
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Blush
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class BodyBlemishes
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class ChestHair
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Complexion
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Component
	{
		public int component_id { get; set; }
		public int drawable { get; set; }
		public int texture { get; set; }
	}

	public class Eyebrows
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class FaceFeatures
	{
		public float cheeksBoneHigh { get; set; }
		public float cheeksBoneWidth { get; set; }
		public float cheeksWidth { get; set; }
		public float chinBoneLenght { get; set; }
		public float chinBoneLowering { get; set; }
		public float chinBoneSize { get; set; }
		public float chinHole { get; set; }
		public float eyeBrownForward { get; set; }
		public float eyeBrownHigh { get; set; }
		public float eyesOpening { get; set; }
		public float jawBoneBackSize { get; set; }
		public float jawBoneWidth { get; set; }
		public float lipsThickness { get; set; }
		public float neckThickness { get; set; }
		public float noseBoneHigh { get; set; }
		public float noseBoneTwist { get; set; }
		public float nosePeakHigh { get; set; }
		public float nosePeakLowering { get; set; }
		public float nosePeakSize { get; set; }
		public float noseWidth { get; set; }
	}

	public class Hair
	{
		public int color { get; set; }
		public int highlight { get; set; }
		public int style { get; set; }
	}

	public class HeadBlend
	{
		public int shapeFirst { get; set; }
		public float shapeMix { get; set; }
		public int shapeSecond { get; set; }
		public int skinFirst { get; set; }
		public float skinMix { get; set; }
		public int skinSecond { get; set; }
	}

	public class HeadOverlays
	{
		public Ageing ageing { get; set; }
		public Beard beard { get; set; }
		public Blemishes blemishes { get; set; }
		public Blush blush { get; set; }
		public BodyBlemishes bodyBlemishes { get; set; }
		public ChestHair chestHair { get; set; }
		public Complexion complexion { get; set; }
		public Eyebrows eyebrows { get; set; }
		public Lipstick lipstick { get; set; }
		public MakeUp makeUp { get; set; }
		public MoleAndFreckles moleAndFreckles { get; set; }
		public SunDamage sunDamage { get; set; }
	}

	public class Lipstick
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class MakeUp
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class MoleAndFreckles
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Prop
	{
		public int drawable { get; set; }
		public int prop_id { get; set; }
		public int texture { get; set; }
	}

	public class SunDamage
	{
		public int color { get; set; }
		public float opacity { get; set; }
		public int style { get; set; }
	}

	public class Tattoos
	{
		public List<ZONEHEAD> ZONE_HEAD { get; set; }
		public List<ZONELEFTARM> ZONE_LEFT_ARM { get; set; }
		public List<ZONELEFTLEG> ZONE_LEFT_LEG { get; set; }
		public List<ZONERIGHTARM> ZONE_RIGHT_ARM { get; set; }
		public List<ZONERIGHTLEG> ZONE_RIGHT_LEG { get; set; }
		public List<ZONETORSO> ZONE_TORSO { get; set; }
	}

	public class ZONEHEAD
	{
		public string collection { get; set; }
		public string hashFemale { get; set; }
		public string hashMale { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string zone { get; set; }
	}

	public class ZONELEFTARM
	{
		public string collection { get; set; }
		public string hashFemale { get; set; }
		public string hashMale { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string zone { get; set; }
	}

	public class ZONELEFTLEG
	{
		public string collection { get; set; }
		public string hashFemale { get; set; }
		public string hashMale { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string zone { get; set; }
	}

	public class ZONERIGHTARM
	{
		public string collection { get; set; }
		public string hashFemale { get; set; }
		public string hashMale { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string zone { get; set; }
	}

	public class ZONERIGHTLEG
	{
		public string collection { get; set; }
		public string hashFemale { get; set; }
		public string hashMale { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string zone { get; set; }
	}

	public class ZONETORSO
	{
		public string collection { get; set; }
		public string hashFemale { get; set; }
		public string hashMale { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string zone { get; set; }
	}
}
