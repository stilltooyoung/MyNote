   M S B A M L   `   `   `  ���� A  =MyNote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MyNoteMyNote      MyNote.MyCalendar   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions   localclr-namespace:MyNote  -     � Title$  
MyCalendar�� � WindowStyle$
 None=�
������  � 	Resources    CalendarStyle1 �� 
TargetType&   System.Windows.Controls.Calendar �� Property �� Value �� 
TargetType ��Name5  /System.Windows.Controls.Primitives.CalendarItem  Name	   CalendarItemStyle  CalendarItemStyle1
 � 	Resources �� StringFormat �Name  Key �� Color'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName �� CornerRadius � Cursor	 �� To J� Opacity �� Stretch F�Name ��Name � RowDefinitions � ColumnDefinitions ��Name  PreviousButtonTemplate  HeaderButtonTemplate  NextButtonTemplate ��Name  DisabledColor  �� RadiusY! �� RadiusX" �� Triggers# P� Property$ P� Value% �� 
TargetName& �� Value' �� Binding( �� RelativeSource) �� AncestorType  CalendarDayButtonStyle1:  4System.Windows.Controls.Primitives.CalendarDayButton*   Transitions%  System.Windows.VisualTransition+   GeneratedDuration, R� KeyTime- R� Value  System.Windows.Visibility. J� RenderTransformOrigin/ J� IsHitTestVisible0 ��Name  CalendarButtonStyle17	  1System.Windows.Controls.Primitives.CalendarButton 	 attentionViewSource��%  &	        &	 �     7  7  7  7  7  &	 �    &	 �    &		 �    �� "  .���  � �� �#f6f6f6��  � �� �� "  .R��� $ 	PART_Root��$��Center=�$��Center=�.+�  $ PART_CalendarItem��#��z��#��z��#��z��#��z	 #��z��#��z��$��Stretch=�$��Stretch=��� "  .���  � �� �� "  
 ���� (��       **(CalendarItem.DayTitleTemplateResourceKey+).R��� ���#666666$��16(�$��微软雅黑)�$��Center=�$
��0 10q�$��Center=����� $ 周{0}��.R�� $ 	PART_Root�� ���� DisabledColor $ 	#A5FFFFFF��   CommonStates��.   
Normal��  Disabled��. �� .��W� $ 0D�$ 1X�$ Opacity�$ PART_DisabledVisual��.+��� ���#cfcfcf$��0q�#��z��$ 2��._��� ._��  ���� PreviousButtonTemplate " ��.R�� $
 Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��. �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��.+��� ���#363636$ 1Q�$
 Fill=�� .+�F� $
 path��"�����5���Ѱ�5h�W��<�h�	���#e0e0e0$
��Left=�$��10��$��10��$��10,0,0,0q�$
 Fill=�$��Center=��� NextButtonTemplate " ��.R�� $
 Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��. �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��.+��� ���#363636$ 1Q�$
 Fill=�� .+�F� $
 path��"����xT��!��xT���S�h4 	���#e0e0e0$��Right=�$��10��$��10��$��0,0,10,0q�$
 Fill=�$��Center=��� HeaderButtonTemplate " ��.R�� $
 Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF73A9D8X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ buttonContent��  Disabled��. �� .��W� $ 0D�$ .5X�$ Opacity�$ buttonContent��.+��� $ buttonContent��#��z��#��z�����#e0e0e0#��z��$��1,4,1,9q�#��z�� �� $
��Auto ��� $��* � �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��3�����#363636�� $ PART_PreviousButton��$��0��|�.  $
��Left=�$��0��8��  �� $ PART_HeaderButton��$��1��$��微软雅黑)�|�.  $��26(�$��Center=�$��0��8�� $��Center=��� $ PART_NextButton��$��2��|�.  $��Right=�$��0��8�� �� $��1��$��3��$��0q����#cfcfcf$��3,0,3,3q�._�� $ PART_MonthView��$��Center=�$y�Visible=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $
��auto ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �� $ PART_YearView��$��3��$��Center=�$��6,-3,7,6q�$��1��$y�Hidden=�$��Center=� �� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $ PART_DisabledVisual��8�� $ 0Q�$  2��$! 2��$
 Fill=�8�� $��1��$y�	Collapsed=�" P� # � {�$ .  . ��� $% PART_DisabledVisual�� � y�$ Visible=��� $
& Year��' �� *DisplayMode+( �� *FindAncestor+")  .a��� $% PART_MonthView�� � y�$ Hidden=��� $% PART_YearView�� � y�$ Visible=��� $& Decade��' �� *DisplayMode+( �� *FindAncestor+")  .a��� $% PART_MonthView�� � y�$ Hidden=��� $% PART_YearView�� � y�$ Visible=��� "  .���  � ��$ 1q���  � ��$ 5����  � ��$ 5����  � ��$ 15(���  � ��$ 微软雅黑)���  � ��$ Center=���  � ��$ Center=���  � �� �� "  .R��    CommonStates��*   $+ 0:0:0.1D�.   
Normal��  	MouseOver��. �� .��W� $ 0D�$	 0.5X�$ Opacity�$ HighlightBackground��  Pressed��. �� .��W� $ 0D�$	 0.5X�$ Opacity�$ HighlightBackground��  Disabled��. �� .��W� $ 0D�$ 0X�$ Opacity�$ HighlightBackground��W� $ 0D�$	 .35X�$ Opacity�$ 
NormalText��  SelectionStates��*   $+ 0D�.   
Unselected��  Selected��. �� .��W� $ 0D�$	 .75X�$ Opacity�$ SelectedBackground���� $ 0D�$ whiteX�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��  CalendarButtonFocusStates��*   $+ 0D�.   CalendarButtonFocused��. �� .��T� $ 0D�$ 
Visibility�$ DayButtonFocusVisual��..�i� $, 0��-  Visible=�  CalendarButtonUnfocused��. �� .��T� $ 0D�$ 
Visibility�$ DayButtonFocusVisual��..�i� $, 0��-  	Collapsed=�  ActiveStates��*   $+ 0D�.   
Active��  Inactive��. �� .���� $ 0D�$ #b4b3b3X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��  	DayStates��*   $+ 0D�.   
RegularDay��  	Today��. �� .��W� $ 0D�$ 1X�$ Opacity�$ TodayBackground���� $ 0D�$ #666666X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��T� $ 
Visibility�$ imgToday��..�i� $, 0��-  Visible=�  BlackoutDayStates��*   $+ 0D�.   	NormalDay��  BlackoutDay��. �� .��W� $ 0D�$ .2X�$ Opacity�$ Blackout��.+��� ���#bbbbbb$��1q�._��� 
�������$��2,2,0,0q�$��1,1,0,0q��� $ TodayBackground�����#c6c6c6$ 0Q�$  1��$! 1���� $ SelectedBackground�����#6eafbf$ 0Q�$  1��$! 1���� #��z��#��z��#��z���� $ HighlightBackground�����	#FFBADDE9$ 0Q�$  1��$! 1���� $ 
NormalText�����#666666#��z��#��z��F� $ Blackout��}�����5 �Z @�J� �Q2� �J� �̉� iV� �A�� �J� �W�� �J� ѷ�� #�� �>8� 7T�K�� 7T�� � ��� ћ�� 7T����G @7T��� �D� 	���	#FF000000$��Stretch=�$��3q�$ 0Q�$. 0.5,0.5&�$
 Fill=�$��Stretch=��� $ DayButtonFocusVisual��/ .  $  1��$! 1�����	#FF45D6FA$y�	Collapsed=��� $0 imgToday��$��25��$��25��$��	Today.png��$	��Top=�$
��Left=�$y�Hidden=��� " 	 .���  � �� �	#FFBADDE9��  � ��$ 60����  � ��$ 60����  � ��$ 15q���  � ��$ 20(���  � ��$ Center=���  � ��$ Center=���  � �� �� " 	 .R��    CommonStates��*   $+ 0:0:0.1D�.   
Normal��  	MouseOver��. �� .��W� $ 0D�$ .5X�$ Opacity�$ 
Background��  Pressed��. �� .��W� $ 0D�$ .5X�$ Opacity�$ 
Background��  SelectionStates��*   $+ 0D�.   
Unselected��  Selected��. �� .��W� $ 0D�$	 .75X�$ Opacity�$ SelectedBackground��  ActiveStates��*   $+ 0D�.   
Active��  Inactive��. �� .���� $ 0D�$ 	#FF777777X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��  CalendarButtonFocusStates��*   $+ 0D�.   CalendarButtonFocused��. �� .��T� $ 0D�$ 
Visibility�$ CalendarButtonFocusVisual��..�i� $, 0��-  Visible=�  CalendarButtonUnfocused��. �� .��T� $ 0D�$ 
Visibility�$ CalendarButtonFocusVisual��..�i� $, 0��-  	Collapsed=�.+��� $ SelectedBackground��#��z��$ 0Q�$  1��$! 1���� $ 
Background��#��z��$ 0Q�$  1��$! 1���� $ 
NormalText�����	#FF333333#��z��$��1,0,1,1q�#��z���� $ CalendarButtonFocusVisual��/ .  $  1��$! 1�����	#FF45D6FA$y�	Collapsed=�" P� # �@J�	IsFocused$ . . ��� $% CalendarButtonFocusVisual�� � y�$ Visible=��� .��� -   $ 
LayoutRoot��$
��Left=�$��755.5��.
  (System.Windows.Interactivity.Interaction1 
  	Behaviors1 H  BMicrosoft.Expression.Interactivity.Layout.MouseDragElementBehavior  .+��� $��	0,0,-60,0q�._�� -   2 �Name$
2 Main��$��0,0,-63.5,112q�$
��Left=�$��763.5��.+�  -   3  Name$3 
myCalendar��#���   #	 �  4   CalendarDayButtonStyle#4 �  5   CalendarButtonStyle#5 �  $��354.5��$��408.5��� -   $
 more��$y�Visible=�$	��401�����	#FF363636$	��407��6 � DataContext#6 [	 .+��� -   7 ��Name$7 showDay��$��40��$	��200��8 �� TextAlignment$8 Center=�$��Today�����	#FF363636���	#FFF6F6F6$��26.667(�$
��Bold"�$��Center=�$	��Top=�� $
��29.5��$��Bottom=�.+��� -   $ creatAttention��$��创建提醒��$	��200��$��30���� -   $ Diary��$��	写日记��$	��200��$��30���� -	   9 ��Name$9 	showDiary��: �� TextAlignment$
: Left=�$!��该日没有心情写日记�����	#FF363636���	#FFF6F6F6$��0,73,0,34.5q�; �� TextWrapping$
; Wrap=�< �� 
IsReadOnly< . $
��Auto=�= �� AutoWordSelection= . 9� -
   > 9�Name$> dayAttentions��? 9� 
IsExpanded? .  $��该日事件��$��0,45,20,34.5q����	#FFF6F6F6@ J� SnapsToDevicePixels@ . A 9� ExpandDirection$
A Down=�$��Stretch=�.��� $
��auto��.+�&   System.Windows.Controls.DataGrid  -   B  Name$B attentionDataGrid��C   AutoGenerateColumnsC .  D   EnableRowVirtualizationD . $��0,3,-2,0q�E   RowDetailsVisibilityMode$E VisibleWhenSelected=�F   
IsReadOnlyF . ���� G   ColumnsG 4  .System.Windows.Controls.DataGridTemplateColumn  -   attention1Column��,  &System.Windows.Controls.DataGridColumnH   Header$H 	attention��I   Width5  /System.Windows.Controls.DataGridLengthConverter$I SizeToHeader J   CellTemplateJ �� .R��� $
��Wrap=����� *
attention1+  -   deadlineColumn��$H deadline��$I SizeToHeader J �� .R��� $
��Wrap=����� *
deadline+  -   warningColumn��$H warning��$I * J �� .R��� $
��Wrap=����� *	warning+� -   $ 
writeDiary��$��0,45,0,0q�$y�Hidden=����	#FF363636.+��� -   $9 contentDiary��K � ToolTip$K 日记内容��$��5,78,5,66.5q�$
; Wrap=�L �� AcceptsReturnL . M �� 
AcceptsTabM . = . $
��Auto=�$
: Left=��� -   $9 
diaryTitle��$��68,22,29,312q�$K 日记标题���� $��32.973,25.24,0,0q����	#FFF6F6F6$
��Left=�$	��Top=�$��23��.��title:�� $��编辑日记：��$
��Left=�$��10,0,0,0q�$	��Top=�$��58�����	#FF363636���	#FFF6F6F6�� -   $ submitDiary��$��创建��$��Bottom=�$
��65.5��$
��Left=�$	��180��$��5,0,0,1q��� -   $ 	backDiary��$��返回��$��Bottom=�$
��65.5��$
��Left=�$��175.5��$��220.5,0,0,0q��� $��168.5,50,168.167,284q����	#FFF6F6F6$
��body��� -   $ addAttention��$��0,45,0,0q�$y�Hidden=����	#FF363636.+��� -   N ��Name$N deadLine��$��截止时间：��$
��Left=�$��10,27.5,0,0q�$	��Top=����	#FF363636���	#FFF6F6F6�� -   $
9 hour��$��25��$��25��$��81,57.5,0,0q�$
; Wrap=�$
K hour��$	��Top=�$
��Left=��� -   $9 minute��$��25��$��25��$��131,57.5,0,0q�$
; Wrap=�$
K hour��$	��Top=�$
��Left=��� $��25��$��25��$	��时��$
��Left=�$��106,57.5,0,0q�$	��Top=����	#FF363636���	#FFF6F6F6�� $��25��$��25��$	��分��$
��Left=�$��156,57.5,0,0q�$	��Top=����	#FF363636���	#FFF6F6F6�� -   $N label��$��提醒时间：��$
��Left=�$��10,99.126,0,0q�$	��Top=����	#FF363636���	#FFF6F6F6�� -   O ��Name$O warning��$��81,129.366,18,0q�$	��Top=�.@��� $��	不提醒���� $��正点���� $��10分钟前���� $��30分钟前���� $��	一天前���� $��	三天前���� $��	一周前���� -   $9 contentAttention��$��81,210.5,18,94.5q�$
; Wrap=�$2K ,字数请控制在25字以内，便于显示���� $��事件内容：��$
��Left=�$��10,170.5,0,0q�$	��Top=�$��70�����	#FF363636���	#FFF6F6F6�� -   $ submitAttention��$��创建��$��Bottom=�$
��65.5��$
��Left=�$	��180���� -   $ backAttention��$��返回��$��Bottom=�$
��65.5��$��Right=�$	��180��