# SetItem

## Description

Set this ItemStack.

Value asigned before will not affect in this 
function and the count limited will  decide 
by SOItemBase.StackLimit.

--- 
###  <font color=#7293A0>int</font> <font color=#CCC066>SetItem</font> (  <font color=#7293A0>SOItemBase</font> <font color=#8CCCFF>SOItemBase</font> ),  <font color=#7293A0>int</font> <font color=#8CCCFF>int</font> )
Set input parametor to the value,  but 
if count bigger than Item's StackLimit,
It  only  set  it  to  the  limit  and return 
overflow count.
