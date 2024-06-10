# Simulated robot NICO and utilization of eye tracking in Unity[^1]

## Quickstart

Download Unity Hub. Click on `Add project from disk` and agree with installation of recomended version for project.

This project is build around `HTC Vive Pro Eye` and it's integrated eye tracking. So necessary to run our project correctly you have to install software:
1. VIVE software from (https://www.vive.com/us/setup/pc-vr/)
    - This includes SRanipal for eye tracking
2. Steam VR from (https://store.steampowered.com/app/250820/SteamVR/)

Follow the instructions on how to confiugure and setup respective applications.
To use eye tracking properly make sure you calibrated it once you put your headset on. You can follow instructions here (https://www.vive.com/ca/support/vive-pro-eye/category_howto/calibrating-eye-tracking.html)

3. To use hand tracking you need `Leap Motion Controller` and software from here (https://leap2.ultraleap.com/downloads/leap-motion-controller/)

Once everything is installed run `Steam VR`, `Ultraleap Tracking` and `VIVE SRanipal`.Then you can open project in `Unity` find `Scenes` folder and double-click `myNicoScene`. Once everything loads you an run the project from interface.

## Notes
If the eye tracking is not working properly check `Unity Console` for warning: _`No conected eye trackers found.`_ This is related to hardware issues and you will need to restart headset and `VIVE SRanipal` runtime. If you are using hand tracking we recomend to unplug it before trying to run the project again.




[^1]: We would like to thank [Iveta Bečková](https://github.com/iveta331/NICO.git) for providing us with the scene with NICO.

