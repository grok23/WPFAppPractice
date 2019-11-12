# WPFAppPractice
first steps with WPF

Noughts and Crosses is an adaptation of an earlier c# console program I made. 

Along the way I've learnt the basics of event handling for buttons/text boxes/radio buttons, how to use TabIndex,  to use TabIndex, PreviewTextInput along with a a validation method to restrict/validate characters entered in a text box, set maxlength, PreviewTextInput along with a validation method/regex to restrict/validate characters entered into a text box, draw lines on the canvas.

I've also learnt that arrays can't be stored as constants and that the way to deal with this is to have an original and then a backup copy which can then be cloned later, so that it never gets altered itself.

Next task:

work out a way to check for stalemate situations on the board, before the board is full. It should be possible to add some more lines to the current winstate checker which look for lines containing both O's and X's and if there is no chance of a win stopping the game early/focusing the board reset button. 
