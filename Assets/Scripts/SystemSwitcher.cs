using UnityEngine;

[ExecuteInEditMode]
public class SystemSwitcher : MonoBehaviour {

	private GameObject activatedSystem;
	public GameObject[] systems = new GameObject[1];

	void OnEnable(){
		for(int i = 0; i < systems.Length; i++) {
			if(systems[i].activeSelf){
				activatedSystem = systems[i];
				break;
			}
		}

	}

	public void Activate(int index){
		if(activatedSystem != null){
			activatedSystem.SetActive(false);
		}
		activatedSystem = systems[index];
		activatedSystem.SetActive(true);
	}
}
