# OverlapGroup

## Description

This class help you to construct a complicated graphic composition, you can obtain 
all colliders which are overlapping it, you can also apply DrawGizmos in OnDrawGizmos
and OnDrawGizmosSelected in Monobehaviour.

## Properties


|Type|Name|Get|Set|Usage|
|:-:|:-:|:-:|:-:|:-:|
|List<CubeData>|CubeDatas|   ■|   ■|the information of the cubes.|
|List<SphereData>|SphereDatas|   ■|   ■|the information of the spheres.|

## Methods

###  <font color=#7293A0>void</font> <font color=#CCC066>DrawGizmos</font> ( 
This function can only be used in OnDrawGizmos and OnDrawGizmosSelected.
###  <font color=#7293A0>Collider[]</font> <font color=#CCC066>GetColliders</font> ( 
Find all colliders touching or inside of the given cubes and spheres.

## Subclass

 - CubeData
 - SphereData
