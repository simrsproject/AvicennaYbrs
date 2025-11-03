<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeartPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.HeartPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

    <uc1:GcsCtl runat="server" ID="gcsCtl" />
    <table width="100%">
        <tr>
            <td class="label">Head</td>
            <td style="width: 150px">
                <asp:RadioButtonList ID="optKepala" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtKepala" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Eyes</td>
            <td>
                <asp:RadioButtonList ID="optMata" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtMata" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">ENT</td>
            <td>
                <asp:RadioButtonList ID="optTht" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtTht" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Mouth</td>
            <td>
                <asp:RadioButtonList ID="optMulut" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtMulut" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Neck</td>
            <td>
                <asp:RadioButtonList ID="optLeher" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtLeher" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Thorax</td>
            <td>
                <asp:RadioButtonList ID="optThorax" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtThorax" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Heart</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtJantung" runat="server" Width="100%" />
            </td>

        </tr>
        <tr>
            <td class="label">•	Inspection</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtInspeksi" runat="server" Width="540px" LabelWidth="180px" Label="Ictus cordis visualized at" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Palpation</td>
            <td colspan="2">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtPalpasi" runat="server" Width="250px" LabelWidth="180px" Label="Ictus cordis punctum max at" /></td>
                        <td>, lifting</td>
                        <td>
                            <asp:RadioButtonList ID="optLifting" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                <asp:ListItem Text="-" Value="-" Selected="true"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>&nbsp;Thrill</td>
                        <td>
                            <asp:RadioButtonList ID="optThrill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                <asp:ListItem Text="-" Value="-" Selected="true"></asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <td class="label">•	Left Percussion </td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtPerkusiLeft" runat="server" Width="540px" LabelWidth="180px" Label="Left heart border at" />
            </td>
        </tr>
        <tr>
            <td class="label">• Right Percussion</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtPerkusiRight" runat="server" Width="540px" LabelWidth="180px" Label="Right heart border at" />
            </td>
        </tr>
        <tr>
            <td class="label">Auscultation</td>
            <td colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 80px">S1S2</td>
                        <td>
                            <telerik:RadTextBox ID="txtAuscultationS1S2" runat="server" Width="100%" /></td>
                        <td>
                    </tr>
                    <tr>
                        <td>Murmur</td>
                        <td>
                            <telerik:RadTextBox ID="txtMurmur" runat="server" Width="100%" /></td>
                        <td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">Lungs</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParu" runat="server" Width="100%" />
            </td>
        </tr>           
        <tr>
            <td class="label">•	Inspection</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParuInspeksi" runat="server" Width="100%" />
            </td>
        </tr>        
        <tr>
            <td class="label">•	Palpasi</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParuPalpasi" runat="server" Width="100%" />
            </td>
        </tr>    
        <tr>
            <td class="label">•	Perkusi</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParuPerkusi" runat="server" Width="100%" />
            </td>
        </tr>   
        <tr>
            <td class="label">•	Auscultation</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParuAusk" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Abdomen</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAbdomen" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Inspection</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAbdoInspeksi" runat="server" Width="100%" />
            </td>
        </tr>        
        <tr>
            <td class="label">•	Palpasi</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAbdoPalpasi" runat="server" Width="100%" />
            </td>
        </tr>    
        <tr>
            <td class="label">•	Perkusi</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAbdoPerkusi" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Genitalia & Anus</td>
            <td>
                <asp:RadioButtonList ID="optGenitaliaAndAnus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtGenitaliaAndAnus" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Ekstremitas</td>
            <td>
                <asp:RadioButtonList ID="optEkstremitas" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtEkstremitas" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Skin</td>
            <td>
                <asp:RadioButtonList ID="optKulit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtKulit" runat="server" Width="100%" />
            </td>
        </tr>  
          <tr>     
            <td>
                <telerik:RadTextBox ID="txtTataLaksana" runat="server" Width="100%" />
           </td>
        </tr>
        <tr>     
            <td>
                <telerik:RadTextBox ID="txtLamaRawat" runat="server" Width="100%" />
           </td>
        </tr>
        <tr>     
            <td>
                <telerik:RadTextBox ID="txtPrognosis" runat="server" Width="100%" />
           </td>
        </tr>
        <tr>
            <td class="label">Notes
            </td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
        </tr>
        <tr id="trNutriSkrinning" runat="server" visible="false">
            <td class="label">Nutrition Skrinning</td>
            <td colspan="2">
                <asp:RadioButtonList ID="optNutritionSkrinning" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                    <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                    <asp:ListItem Text="Malnutrition" Value="Malnutrition"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
