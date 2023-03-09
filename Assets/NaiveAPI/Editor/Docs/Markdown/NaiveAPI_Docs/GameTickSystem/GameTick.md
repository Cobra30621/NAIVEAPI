# GameTick

## Description

It is a Singleton GameObject.
With this manager, you can controll
scripts that contain ITickUpdate interface.

Controllable things
  - Game Speed
  - Script’s update frequency

--- 
## Properties


|Type|Name|Get|Set|Usage|
|:-:|:-:|:-:|:-:|:-:|
|int|TickPerSec|   ■||How many Tick in a secend|
|int|TickRate|   ■|   ■|How many TickRate in a secend|
|float|GameSpeed |   ■|   ■|TickRate / TickPerSec|
|int|CurrentTick|   ■||Tick in runtime|
|int|CurrentRealTick|   ■||RealTick in runtime|
|float|CurrentTime|   ■||Time since GameTick startup (sec)|
|float|DeltaTime|   ■||Same as Time.deltaTime|
|float|DeltaTickTime|   ■||DeltaTime through this Tick|


--- 
## UpdateList Data


|Type|Name|Usage|
|:-:|:-:|:-:|
|List<ITickUpdate>|TickUpdatesList|Func update with RealTick|
|List<ITickUpdate>|TickRateUpdatesList|Func update with TickRate|
|List<SpecialTickUpdate>|SpecialTickUpdatesList|Func update with custom frequence in TickRate|


--- 
## Methods Overview

###  <font color=#7293A0>bool</font> <font color=#CCC066>GetKey</font> ( 

###  <font color=#7293A0>bool</font> <font color=#CCC066>GetKeyUp</font> ( 

###  <font color=#7293A0>bool</font> <font color=#CCC066>GetKeyDown</font> ( 

