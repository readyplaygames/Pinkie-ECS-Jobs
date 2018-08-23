# Pinkie-ECS-Jobs
A demonstration of Unity's Entity Component and Jobs Systems

## Issues

- For reasons unknown, ECS cannot use an unlit shader for rendering the images, so a different material must be used
- IJobProcessComponentData can only process three parameters, also for reasons unknown, so a different Entity Component must be used to avoid conflicts.
