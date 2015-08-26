# Demolisher v0.3.1

## Summary

_Demolisher_ an extremely basic viewer for the BIN model format found in [Luigi's Mansion](http://en.wikipedia.org/wiki/Luigi%27s_Mansion).
The program is capable of opening and viewing one or multiple BIN model files at one time and comes with a bunch of different view modes and options.
Do **not** expect the program to be able to export or convert these models into another format and vice versa.

This program makes use of [OpenTK](http://www.opentk.com) for rendering and input.

## Use

### Controls

To move the main view camera, use the ESDF keys for forward, leftward, backward, and rightward movement respectively.
Holding shift while pressing any of the movement keys while double the camera's speed.
To rotate the camera's pitch and yaw, use the arrow keys.
There is an option to invert the pitch controls.

### Render Flags

There are certain flags and data in the BIN model format for graph objects which are displayed in _Demolisher_ (some of which can be toggled).
The supported ones are as follows:

- Ceilings
- Fourth walls (or any object that is shown only in the GBH view)
- Full-bright (such as lamps)
- Bounding boxes

As well as the visibility flags above, you can set the individual visibility of each loaded BIN model and their graph objects.

### Support

_Demolisher_ also supports the following:

- NBT (normal/binormal/tangent) vector display on vertices
- Wireframe view
- Unit grid (X/Z axis)
- Texture alpha (cut-off and blend)
- Emboss-mapped surfaces (`emboss.fp` and `emboss.vp`)

_Demolisher_ supports _only_ BIN models — BDL and BMD are not supported; however, you may open and view any number of BIN model files at a single time.
