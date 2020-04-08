using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; //이름
    public GameObject go_Prefab; // 실제 설치될 프리팹
    public GameObject go_PreviewPrefab; // 미리보기 프리팹
}

public class CraftManual : MonoBehaviour {

    //상태변수
    private bool isActivated = false;
    private bool isPriviewActivated = false;

    [SerializeField]
    private GameObject go_BaseUI; //기본적인 UI

    [SerializeField]
    private Craft[] craft_fire; //모닥불용 탭

    private GameObject go_Preview; //미리보기 프리팹을 담을 변수
    private GameObject go_Prefab; //실제 생성될 프리팹을 담을 변수


    [SerializeField]
    private Transform tf_Player; //플레이어 위치

    //Raycast 필요 변수 선언
    private RaycastHit hitinfo;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;


    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(craft_fire[_slotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = craft_fire[_slotNumber].go_Prefab;
        isPriviewActivated = true;
        go_BaseUI.SetActive(false);
    }


	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab) && !isPriviewActivated)
            Window();

        if (isPriviewActivated)
            PriviewPositionUpdate();

        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
	}

    private void Build()
    {
        if (isPriviewActivated && go_Preview.GetComponent<PreviewObject>().IsBuildable())
        {
            Instantiate(go_Prefab, hitinfo.point, Quaternion.identity);
            Destroy(go_Preview);
            isActivated = false;
            isPriviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
        }
    }

    private void PriviewPositionUpdate()
    {
        if(Physics.Raycast(tf_Player.position, tf_Player.forward, out hitinfo,range,layerMask))
        {
            if(hitinfo.transform != null)
            {
                Vector3 _location = hitinfo.point;
                go_Preview.transform.position = _location;
            }
        }
    }

    private void Cancel()
    {
        if (isPriviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPriviewActivated = false;
        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);
    }

    private void Window()
    {
        if(!isActivated)
            OpenWindow();
        else
            CloseWindow();
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }


}
