# CSCI 49383 Final Project
## Sharron Qian


## Before you download
This project uses SteamVR and the Valve.VR namespace for input from an HTC Vive Pro headset and controllers. Any scripts that get user input (object interaction and player movement) specifically get input from a Vive controller. As I have not tested the project with other VR softwares, I am unsure of how it would function with other softwares.

## If not running on load:
Some assets may have been unassigned from components in the project. Below is a list of instructions of where in the project to add assets from the assets folder.
## **Assets**
<details>
    <summary>Audio</summary>
    - Free UI Click Sound Effects -> AUDIO -> Button -> SFX_UI_Button_Organic_Plastic_Generic_1
        - Place the sound into the AudioClip inside Audio Source of the Collider underneath each Button.
    - 04 honey lemon tea.wav
        - Place the music into the AudioClip in the Audio Source of the Music Player. 
        - Place the music into the Music variable of the PlayMusic.cs script of the Music Player.
</details>
<details>
    <summary>Avatars</summary>
    - Drag Dancing Mouse game object from hierarchy into Mouse variable of PlayMusic.cs script of the Music Player.
</details>

## **Others**
<details>
    <summary>Basketball Game</summary>
    - Drag Ball Respawn Point from hierarchy into the Table variable in the BasketballGame.cs script in the Collider under the Trash can in the Furniture game object.
    - In the same script, drag the Ball from the hierarchy into the Ball variable.
</details>
