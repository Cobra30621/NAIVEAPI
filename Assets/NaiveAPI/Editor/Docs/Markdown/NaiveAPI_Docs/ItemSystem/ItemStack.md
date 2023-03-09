# ItemStack

## Description

A struct to save items data,
notice that it is a ValueType, 
means it send dupliecate to other when calling it.

--- 
## Properties


|Type|Name|Get|Set|Usage|
|:-:|:-:|:-:|:-:|:-:|
|SOItemBase|Item|   ■|   ■|the item store in this ItemStack|
|int|Count|   ■|   ■|how many item in this ItemStack|
|bool|IsFull|   ■||return true if Count >= Item's StackLimit|
|bool|IsEmpty|   ■||return true if Count <=0 or Item is Null|


--- 
## Opreator


|( int )|return count value|
|:-:|:-:|
|( bool )|return IsEmpty|


--- 
## Methods

###  <font color=#7293A0>void</font> <font color=#CCC066>Clear</font> ( 

###  <font color=#7293A0>int</font> <font color=#CCC066>SetItem</font> ( 

###  <font color=#7293A0>int</font> <font color=#CCC066>PutItem</font> ( 

###  <font color=#7293A0>int</font> <font color=#CCC066>TakeItem</font> ( 

