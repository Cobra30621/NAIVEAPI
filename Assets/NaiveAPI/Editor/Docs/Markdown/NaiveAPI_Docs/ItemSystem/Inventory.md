# Inventory

## Description

With this class, you can controller item IO easily.
- Feture
    - Item IO
    - Item Search
    - Item Crafting
    - Custom Inspector

--- 
## Properties


|Type|Name|Get|Set|Usage|
|:-:|:-:|:-:|:-:|:-:|
|Action|OnItemChanged|   ■|   ■|Called when Inventory's state changed.|
|ItemStack[ ]|Slots|   ■|   ■|use as Inventory's slot.|
|int|Capacity|   ■|||
|int|Size|   ■||Inventory's useable size.|
|int|Count|   ■||How many slots has been used.|


--- 
## Methods

###  <font color=#7293A0>ItemStack</font> <font color=#CCC066>Push</font> ( 

###  <font color=#7293A0>ItemStack</font> <font color=#CCC066>PushAt</font> ( 

###  <font color=#7293A0>void</font> <font color=#CCC066>Pop</font> ( 

###  <font color=#7293A0>void</font> <font color=#CCC066>PopAt</font> ( 

###  <font color=#7293A0>bool</font> <font color=#CCC066>TryPop</font> ( 

###  <font color=#7293A0>bool</font> <font color=#CCC066>TryPopAt</font> ( 

###  <font color=#7293A0>void</font> <font color=#CCC066>Swap</font> ( 

###  <font color=#7293A0>List<int></font> <font color=#CCC066>FindItem</font> ( 

###  <font color=#7293A0>List<int></font> <font color=#CCC066>FindItemByTag</font> ( 

###  <font color=#7293A0>int</font> <font color=#CCC066>CountItem</font> ( 

###  <font color=#7293A0>void</font> <font color=#CCC066>Sort</font> ( 

