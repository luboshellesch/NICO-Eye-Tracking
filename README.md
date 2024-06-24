# Simulated robot NICO and utilization of eye tracking in Unity[^1]

## Quickstart

Download Unity Hub. Click on `Add project from disk` and agree with installation of recomended version for project.

This project is build around `HTC Vive Pro Eye` and it's integrated eye tracking. So necessary to run our project correctly you have to install software:
1. VIVE software from (https://www.vive.com/us/setup/pc-vr/)
    - This includes SRanipal for eye tracking
2. Steam VR from (https://store.steampowered.com/app/250820/SteamVR/)

Follow the instructions on how to confiugure and setup respective applications.
To use eye tracking properly make sure you calibrated it once you put your headset on. You can follow instructions here (https://www.vive.com/ca/support/vive-pro-eye/category_howto/calibrating-eye-tracking.html)

3. Tobii Eye Tracking SDK from (https://developer.tobii.com/download-packages/tobiixr-sdk-3-0-1/)
    - Extract the content to a file and copy the path. Then, navigate to `NICO-Eye-Tracking-main\Packages\manifest.json` and `NICO-Eye-Tracking-main\Packages\packages-lock.json`. You will find `C:/Example/Path/To/TobiiXRSDK` there, where you should paste the path.

4. To use hand tracking you need `Leap Motion Controller` and software from here (https://leap2.ultraleap.com/downloads/leap-motion-controller/)

Once everything is installed run `Steam VR`, `Ultraleap Tracking` and `VIVE SRanipal`.Then you can open project in `Unity` find `Scenes` folder and double-click `myNicoScene`. Once everything loads you an run the project from interface.

## How to use
When the eye-tracker is activated, NICO will respond to your gaze while you play the scene. If you look at one of the cubes, NICO will turn his head to follow your gaze. If you make eye contact with him, he will look back at you. If you focus on his lower arm, he will wave at you.

Additionally, we've incorporated basic UI controls using hand tracking. When this feature is enabled, rotating your left hand will display the UI. You can then press buttons with your right hand. Furthermore, you can move objects in the scene using your hands.


## Notes
If the eye tracking is not working properly check `Unity Console` for warning: _`No conected eye trackers found.`_ This is related to hardware issues and you will need to restart headset and `VIVE SRanipal` runtime. If you are using hand tracking we recomend to unplug it before trying to run the project again.

### FIX#1 [^2]

Disconnect all the link box cables from the computer.

Navigate to the `drop down menu` on `Steam VR` => `Developer` => `Developer settings`.

Click `Remove all Steam VR USB devices`. Make sure the USB cable or other cable is not connected to the link box and click `Yes`.

Once this is complete, exit `Steam VR`.

Restart your computer.

Reconnect the USB cables. This will reinstall all `Vive USB Drivers`.

### FIX#2 [^2]

Instead of using `USB3.0` or higher plug headset to a `USB2.0`. This should always work.


[^1]: We would like to thank [Iveta Bečková](https://github.com/iveta331/NICO.git) for providing us with the scene with NICO.
[^2]: Provided by (https://www.reddit.com/r/Vive/comments/10ypn77/vive_face_facial_tracker_problems_solved_error/)
