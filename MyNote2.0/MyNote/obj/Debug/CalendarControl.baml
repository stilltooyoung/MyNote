   M S B A M L   `   `   `  ����A  =MyNote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MyNoteMyNote  5      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5	          MyNote.CalendarControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:MyNote     5      Twinform@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5	      $	��392��5      $	��357��6   .��5
      � 
�������  � RowDefinitions  5   
   �� 5      $��35 �6   6*   �� 5      6   .+�5   
   � $��0��6   .+�5      �� -   5        $
��left��5      $
��Left=�6!   $��30��6<   $	��←��6G   $��0,5,0,4q�6S   6d   �� 5      -   5        $��ym��5      $��Center=�6"   $	��100��6@   $��25��6L    �� TextAlignment$ Center=�6X   $��20(�6o   6   �� 5      -   5        $��right��5      $��Right=�6"   $��30��6>   $��25��6I   $	��→��6U   6w   5      7  1System.Windows.Forms.Integration.WindowsFormsHost  5   
   -   5        $��windowsFormsHost��5   (   $��1��6      
Background
 �����   Child. 5      '  !System.Windows.Forms.DataGridView  "  System.Windows.Forms.Control   Name$ calendarDataGrid��6#      ReadOnly .    	AllowDrop .    AllowUserToAddRows .     AllowUserToDeleteRows .  	   FontU QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"  System.Drawing.FontConverter$	 15 5   #   
   RowHeadersWidth$
 50��6-      SelectionMode$ 
CellSelect=�6B      ShowEditingIcon .     ShowCellToolTips .     ShowCellErrors .     ShowRowErrors .     AutoSizeColumnsMode$
 None=�5   9      ColumnHeadersHeight$ 50��6T      
ScrollBars$
 None=�6m      Width$	 500��6      Height$	 500��6�   5      5      5      5      