# Virtual &amp; Augmented Reality Assignment

## Executing through Unity

The different levels of the project, given by the different problem tasks, can be executed through selecting the 'Distortion' prefab under the hierarchy view, and manipulating its parameters in the inspector view.

N.B. If the aforementioned prefab does not appear in the hierarchy view, navigate to the 'Assets' directory within the project view, and drag and drop both the 'Distortion' and 'Inverse' prefabs (indicated by blue cubes) into the hierarchy view.

The parameters available to the user to play with are as follows (these should be selected before you press play on the scene, as this generates the most efficient performance):
- **Distortion Type**
  - This parameter selects the type of distortion to be implemented, following each of the requested problem requirements.
  - Possible values:
    - 0 = no distortion (problem 0)
    - 1 = pixel-based distortion (problem 1)
    - 2 = LCA pre-correction (problem 2)
    - 3 = mesh-based distortion (problem 3)
    - 4 = mesh-based distortion with a 2nd mesh and 3rd camera to invert the image again (problem 3c)
- **No Cubes**
  - This controls the number of cubes rendered to the scene, in the vertical (Y) and horizontal (X) directions.
  - Possiblve values: any integer i >= 0
- **Angle Per Second**
  - The angle at which the cubes within the grid rotate around the X, Y, and Z axes per second.
  - Possible values: any positive or negative float
- **Red Shift**, **Green Shift**, **Blue Shift**
  - The extent of chromatic aberration to the red, green, and blue colour channels (both laterally X and vertically Y).
  - N.B. only takes effect if *Distortion Type = 2* is selected.
  - N.B.B this is the only parameter which can be manipulated during runtime to generate observable results.
  - Possible values: any positive or negative float
- **Mesh Complexity**
  - The complexity of the mesh to which the scene is projected during mesh-based distortion.
  - N.B. only takes effect if *Distortion Type = 3 or 4* is selected.
  - Possible values:
    - 0 = low complexity mesh of 9 vertices
    - 1 = intermediate complexity mesh with 121 vertices
    - 2 = high complexity mesh with 7921 vertices
    
    
## File Structure

Alongside the required Unity files, the submission contains a number of additonal files within the Assets directory used for each problem.

- *research_report.pdf*
- *Unity/Assets*:
  - *camera.cs* = script for applying distortions to the camera (problems 1 + 2)
  - *distortion.cs* = the main script containing the majority of the code implemented (for problems 0-3)
  - *inverse_camera.cs* = script for initialising the 3rd camera used during the 2nd inverse mesh-based distortion (problem 3c)
  - *Resources/Planes*
    - *plane1.blend* = 9 vertex mesh
    - *plane2.blend* = 121 vertex mesh
    - *plane3.blend* = 7921 vertex mesh
  - *Resources/Shaders*
    - *basic.shader* = shader used for the camera when no effects are applied, or mesh-based is used (problems 0 + 3)
    - *cubes.shader* = shader used to colour apply texture to the cubes within the scene (problem 0)
    - *inverse_mesh.shader* = shader used for applying the mesh-based vertex shader transforms on the 2nd mesh (problem 3c)
    - *LCA_correction.shader* = shader used for applying the colour shifting of LCA correction (problem 2)
    - *mesh_distortion.shader* = shader used for applying the mesh-based vertex shader transforms on the 1st mesh (problem 3)
    - *pincushion_distortion.shader* = shader used for applying pincushion distortion via the pixel-based method through the fragment shader (problem 1)
    
  
