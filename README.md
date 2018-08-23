# Pinkie ECS Jobs

A serious demonstration of the Unity Jobs and Entity Component System (ECS) using a particular pink pony. 

Each test was divided up by number of Pinkies and the type of system used:

- MonoBehaviour (Mono)
- Jobs
- Entity Component System (ECS)
- ECS + Jobs

The scripts running are simple: bounce around in circles. No physics, no audio, and no lighting on the objects.

If you're interested in what else I've done, check out Proxy - Ultimate Hacker: 
http://store.steampowered.com/app/528860

## Issues

- For reasons unknown, ECS cannot use an unlit shader for rendering the images, so a different material must be used
- IJobProcessComponentData can only process three parameters, also for reasons unknown, so a different Entity Component must be used to avoid conflicts.
