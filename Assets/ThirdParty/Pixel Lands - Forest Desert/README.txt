Pixel Lands - Forest & Desert v1.0.0
------------------------------

Thank you for downloading my asset pack. This pack contains top down 16x16 assets for forest and desert environments.

Asset Store Link: https://assetstore.unity.com/packages/slug/304532


SETUP INSTRUCTIONS - Built-in Render Pipeline
----------------------------

1. Transparency Sort Mode. This setting allows sprites in top down games to overlap in the correct order (eg. allows a character to walk both behind and in front of a tree)
   Options are found in Edit > Project Settings > Graphics > Transparency Sort Mode. Use the following settings:
	- Transparency Sort Mode set to `Custom Axis`
	- Transparency Sort Axis set to X = 0, Y = 1, Z = 0

2. Pixel Perfect Camera. Add the `Pixel Perfect Camera` component to the camera object in your scene to avoid weird, uneven pixels and make the pixel art look crisp. Check the camera in the demo scene for an example.

3. Import settings. Sprites in this pack already have the correct import setting but in general, always import pixel art sprites with the following settings:
	- Filter Mode set to `Point(no filter)`
	- Compression set to `None`
	- Pixels Per Unit set based on the intended tile size (16 for everything in this pack)


SETUP INSTRUCTIONS - URP
----------------------------

1. Transparency Sort Mode. This setting allows sprites in top down games to overlap in the correct order (eg. allows a character to walk both behind and in front of a tree)
   Options are found on the Renderer2D object under General > Transparency Sort Mode. Use the following settings:
	- Transparency Sort Mode set to `Custom Axis`
	- Transparency Sort Axis set to X = 0, Y = 1, Z = 0

2. Pixel Perfect Camera. Add the `Pixel Perfect Camera` component to the camera object in your scene to avoid weird, uneven pixels and make the pixel art look crisp. Use the `Upgrade Pixel Perfect Camera` button on the camera object in the demo scene to convert it for URP.

3. Import settings. Sprites in this pack already have the correct import setting but in general, always import pixel art sprites with the following settings:
	- Filter Mode set to `Point(no filter)`
	- Compression set to `None`
	- Pixels Per Unit set based on the intended tile size (16 for everything in this pack)


PACKAGE CONTENTS
--------------------

Tiles and Objects:
- Terrain tiles (grass, sand, paths, dirt, water, hills)
- Wooden fence
- Stone fence
- Desert ruins
- Animated water
- Large objects: trees, bushes, logs, wooden bridge, crates, rocks, etc.
- Small objects: signs, mushrooms, flowers, cacti, gems, grasses, berries, etc.
- Two versions of each object (with/without shadows)
- Rule Tiles: 21 premade rule tiles for water, paths, etc.
- Animated Tiles: campfire, grass

Critters:
- Birds with fly, peck & hop animations
- Butterflies with fly animations
- Beetles with walk animations
- Snakes with walk animations


CONTACT
---------
Contact: trislingames@gmail.com
Instagram: https://www.instagram.com/trislingames/
Twitter: https://twitter.com/trislingames
Bluesky: https://bsky.app/profile/trislingames.bsky.social
