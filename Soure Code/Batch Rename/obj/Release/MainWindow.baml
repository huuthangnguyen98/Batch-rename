   M S B A M L   `   `   `  ���� G  CBatch Rename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Batch_RenameBatch_Rename      Batch_Rename.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Batch_Rename     -     � Icon$  /Icon/icon.png�� � Title$ Batch Rename��$	��650��$
��1100�� � WindowStartupLocation$ CenterScreen=�.��� 
������� � ColumnDefinitions �� �� $��3* � � RowDefinitions �� $��40 ��� .+���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+�`� .�c� $	��250��$��30��.@��� -   $��refreshButton��.���� $ 
Horizontal=�.+��� $��/Icon/refresh.png��$��23��$��18���� $��Refresh���� -   $��previewButton��.���� $ 
Horizontal=�.+��� $��/Icon/preview.png��$��20��$��25���� $��Preview���� -   $��startBatchButton��.���� $ 
Horizontal=�.+��� $��/Icon/play.png��$��18��$��23���� $��Start��� $��1��$��0��
�������$��3q� �� $��5* ��� $��1* �.+�� $��0�� �� $��60 ��� $��25 ��� �� $��30 �.+��� 
�������$��1q�$��0��._��  �� $��3* ���  �� �� .+��� -   $��methodCombobox��$��0��$��0�� �� SelectedIndex$ 0��$��1q�.@��� $y�	Collapsed=�.��Select method�� -   $��repalceComboboxItem��.��	Replace�� -   $��newCaseComboboxItem��.��
New Case�� -   $#��fullNameNormalizeComboboxItem��.��Fullname Normalize�� -	   $��moveComboboxItem��.��Move�� -
   $��uniqueComboboxItem��.��Unique name�� -   $��presetCombobox��$��1��$��0��$��1q�$ 0�� � ToolTip$ Your saved presets���� -   $��	AddMethod��$��0��$��1��$��2q�$	��Add���� -   $��	AddPreset��$��1��$��1��$��2q�$	��Add���� -   $��editPresetListButton��$��1��$��2q�$	��100��$��Edit presets list���� $��2��$ 
Horizontal=�.+��� -   $��methodListview��$	��239������ .R��� $ Vertical=����� .R��� $ 
Horizontal=�.+��� ���� *	Setting+ � ContextMenu �� .@�m� -   $��
editMethod��$
��Edit��m� -   $��removeMethod��$��Remove��m� -   $��removeAllMethod��$��
Remove all��`� 	 `� Orientation$	 Vertical=�$��28��$��	0,0,0,167q�.�c� $��28��$��	0,0,0,-56q�.@��� -   $	��Top��$ Move to top��$��	0,15,0,15q�.���� $��/Icon/top.png��$��15��$��15���� -   $��Up��$ Up��.���� $��/Icon/up.png��$��15��$��15���� -   $
��Down��$
 Down��$��	0,15,0,15q�.���� $��/Icon/down.png��$��15��$��15���� -   $��Bottom��$ Move to end��$��0,0,0,15q�.���� $��/Icon/bottom.png��$��15��$��15���� $��3��.+��� -   $��savePresetButton��$��82��
 �� Right$
 4��
 �� Top$ 5�� �� Left$ 83��$��25��.���� $ 
Horizontal=�.+��� $��/Icon/save.png��$��15��$��25���� $
��Save���� $��1��
�������$��1q�$��4q�._��� .+��� $��Get more information :���� $ ��huuthangnguyen98@gmail.com��$ 10��$ 26���� $��Icons by Flaticon.com��$ 10��$ 69��$��9(�� $��1��$��1��
�������$��3q� �� .+��� $��0��$��3q�.@��� -   $��renameFilesTabItem��$��Rename Files��.���  �� $��30 ��� .+��� $ 
Horizontal=�.+��� -   $��addFilesButton��$	��Add��$��75��$��4q��� -   $��filesListview�� �� SelectionMode$ Single=�$
��10,4q�$��1��$��Disabled=� �� View �� .P��� $��Filename�� �� Width$	 220�� �� DisplayMemberBinding �� *Name+�� $��New Filename��$	 220�� �� *	NewName+�� $
��Path��$	 220�� �� *Path+�� $��Error��$	 100�� �� *Error+ �� .@�m� -   $��
removeFile��$��Remove��m� -   $��removeAllFile��$��
Remove all���� -   $��renameFoldersTabItem��$��Rename Folders��.���  �� $��30 ��� .+��� $ 
Horizontal=�.+��� -   $��addFolderButton��$��75��$	��Add��$��4q��� -   $��folderListview��$ Single=�$
��10,4q�$��1��$��Disabled=� �� .P��� $��
Foldername��$	 220�� �� *Name+�� $��New Foldername��$	 220�� �� *	NewName+�� $
��Path��$	 220�� �� *Path+�� $��Error��$	 100�� �� *Error+ �� .@�m� -    $��removeFolder��$��Remove��m� -!   $��removeAllFolder��$��
Remove all��