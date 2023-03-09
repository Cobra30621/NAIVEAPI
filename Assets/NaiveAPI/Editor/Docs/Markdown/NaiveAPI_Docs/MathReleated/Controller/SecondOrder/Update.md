# Update

## Description

This is main function of the controller. While  update you 
will call it again and again. By calculate with input value,
it return new PositionY.

How often updates are called will affect overall controller
performance.  Calling  it  in  FixUpdate()  will  have  better 
results, or  you can use GameTickSystem's  TickUpdate().

--- 
###  <font color=#7293A0>float</font> <font color=#CCC066>Update</font> (  <font color=#7293A0>float</font> <font color=#8CCCFF>float</font> )

###  <font color=#7293A0>float</font> <font color=#CCC066>Update</font> (  <font color=#7293A0>float</font> <font color=#8CCCFF>float</font> ),  <font color=#7293A0>float</font> <font color=#8CCCFF>float</font> )


|targetValue|The target position you want to go|
|:-:|:-:|
|spd|Change Controller's spd or use old one|
|return|New PositionY|

