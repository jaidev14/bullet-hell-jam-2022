using UnityEngine;
using System.Collections;

public class CameraCinematicController : MonoBehaviour{
	public Cinemachine.CinemachineVirtualCamera cameraFX;
    public Cinemachine.CinemachineVirtualCamera cameraMain;

	void Start(){
        StartCinematic();
    }

    public void StartCinematic() {
        cameraMain.Priority = 0;
        cameraFX.Priority = 1;
    }

    public void StopCinematic() {
        cameraMain.Priority = 1;
        cameraFX.Priority = 0;
    }
}