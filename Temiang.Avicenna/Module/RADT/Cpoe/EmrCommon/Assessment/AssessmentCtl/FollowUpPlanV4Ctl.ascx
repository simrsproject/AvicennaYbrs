<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FollowUpPlanV4Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.FollowUpPlanV4Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend>FOLLOW UP PLAN</legend>
    <table style="width: 100%">
          <tr>
            <td class="label">Control Plan</td>
            <td>
                <telerik:RadTextBox ID="txtControlPlan" runat="server" Width="100%" Height="40px" />

            </td>
            <td></td>
        </tr>
          <tr>
            <td class="label">Consul To</td>
            <td>
                <div>
                    <asp:CheckBoxList ID="optConsulToType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="PD" Text="Penyakit Dalam"></asp:ListItem>
                        <asp:ListItem Value="BDH" Text="Bedah"></asp:ListItem>
                        <asp:ListItem Value="OBG" Text="Obgyn "></asp:ListItem>
                        <asp:ListItem Value="ANK" Text="Anak"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <div>
                      <asp:CheckBox ID="chkInputManually" runat="server" Text="Lainnya"   />
                </div>
                    <div id="manualInputGroup" runat="server">
                        <div>
                            <telerik:RadTextBox ID="txtConsulTo" runat="server"></telerik:RadTextBox>
                        </div>
                </div>                
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Follow Up Plan</td>
            <td>
                <div>
                      <asp:CheckBox ID="chkIPR" runat="server" Text="Rawat Inap"  />
                </div>
                    <div id="manualInputGroup2" runat="server">
                        <div>
                            <telerik:RadTextBox ID="txtInpatientIndication" runat="server"></telerik:RadTextBox>
                        </div>
                </div>   
                <div>
                    <asp:CheckBoxList ID="optFollowUpPlanType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="OPR" Text="Rawat Jalan"></asp:ListItem>                        
                    </asp:CheckBoxList>
                </div>
                <div>
                      <asp:CheckBox ID="chkRJK" runat="server" Text="Rujuk" />
                </div>
                    <div id="manualInputGroup3" runat="server">
                        <div>
                            <telerik:RadTextBox ID="txtReferToHospital" runat="server"></telerik:RadTextBox>
                        </div>
                </div>    
                 <div>
                    <asp:CheckBoxList ID="optFollowUpPlanType2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="PRM" Text="Promotif"></asp:ListItem>   
                        <asp:ListItem Value="PRV" Text="Preventif"></asp:ListItem>   
                        <asp:ListItem Value="KRT" Text="Kuratif"></asp:ListItem>   
                        <asp:ListItem Value="RHB" Text="Rehabilitatif"></asp:ListItem>   
                    </asp:CheckBoxList>
                </div>
            </td>
            <td></td>
        </tr>           
    </table>
</fieldset>


