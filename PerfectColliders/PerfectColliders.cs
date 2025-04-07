using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;

namespace PerfectColliders;
//More info on creating mods can be found https://github.com/resonite-modding-group/ResoniteModLoader/wiki/Creating-Mods
public class PerfectColliders : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.0"; //Changing the version here updates it in all locations needed
	public override string Name => "PerfectColliders";
	public override string Author => "__Choco__";
	public override string Version => VERSION_CONSTANT;
	public override string Link => "https://github.com/resonite-modding-group/ExampleMod/";

	public override void OnEngineInit() {
		Harmony harmony = new Harmony("com.__Choco__.PerfectColliders");
		harmony.PatchAll();
	}

	//Example of how a HarmonyPatch can be formatted, Note that the following isn't a real patch and will not compile.
	[HarmonyPatch(typeof(MeshRenderer), methodName: "BuildInspectorUI")]
	class patchMethods {
		static void Postfix(ref MeshRenderer __instance) {
			Msg("this works here");

			Slot rootSlot = __instance.Slot.World.RootSlot;

			System.Collections.Generic.List<MeshRenderer> meshRenderers = rootSlot.GetComponentsInChildren<MeshRenderer>();
			Msg(meshRenderers.ToString());
			foreach (MeshRenderer meshRenderer in meshRenderers) {
				//if (__instance is not null && __instance.Slot is not null && __instance.Slot.World.Name is not "Userspace") {
					localCollider(meshRenderer);
				//}
			}

			
			

			//collider.
			Msg("Postfix from ExampleMod");
		}


		static void localCollider(MeshRenderer meshRenderer) {
			Msg("lc1");
			if (meshRenderer == null) {
				return;
			}
			Msg("lc2");
			Slot slot = meshRenderer.Slot;
			Msg("lc3");
			MeshCollider collider;
			Msg("lc4");
			if (slot.GetComponent<MeshCollider>((MeshCollider c) => c.Mesh.Target == meshRenderer.Mesh.Target && c.Enabled, false) == null) {
				Msg("lc5");
				if (slot.ActiveUserRoot != null) {
					Msg("lc6");
					return;
				}
				Msg("lc7");
				collider = slot.AttachComponent<MeshCollider>(true, null);
				Msg("lc8");
				collider.Mesh.Target = meshRenderer.Mesh.Target;
				Msg("lc9");
				collider.Persistent = false;
				Msg("lc10");
				collider.SetCharacterCollider();
				Msg("lc11");
				collider.Type.Value = ColliderType.Static;
				Msg("lc12");
				Msg(collider.ToString());
			}
		}
	}
}
