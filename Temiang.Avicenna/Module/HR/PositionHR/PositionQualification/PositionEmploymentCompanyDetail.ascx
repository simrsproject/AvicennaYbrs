<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PositionEmploymentCompanyDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionEmploymentCompanyDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionEmploymentCompany" runat="server" ValidationGroup="PositionEmploymentCompany" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionEmploymentCompany"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPositionEmploymentCompanyID" runat="server" Text="Position Employment Company ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPositionEmploymentCompanyID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionEmploymentCompanyID" runat="server" ErrorMessage="Position Employment Company ID required."
                    	        ControlToValidate="txtPositionEmploymentCompanyID" SetFocusOnError="True" ValidationGroup="PositionEmploymentCompany" Width="100%">
						        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRequirement" runat="server" Text="Requirement"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRRequirement" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRequirement" runat="server" ErrorMessage="Requirement required."
                    	        ControlToValidate="cboSRRequirement" SetFocusOnError="True" ValidationGroup="PositionLicense" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblYearOfService" runat="server" Text="Year Of Service"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtYearOfService" runat="server" Width="300px" NumberFormat-DecimalDigits=0/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvYearOfService" runat="server" ErrorMessage="Year Of Service required."
                    	        ControlToValidate="txtYearOfService" SetFocusOnError="True" ValidationGroup="PositionEmploymentCompany" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
				<tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PositionLicense" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PositionLicense"
                            Visible='<%# DataItem is GridInsertionObject %>'>
				        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel">
				        </asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="300px" 
                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true" 
                            AutoPostBack="false" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                            OnItemsRequested="cboPositionGradeID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PositionGradeCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade Name required."
                            ValidationGroup="entry" ControlToValidate="cboPositionGradeID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr> 
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="80px" MaxLength="400" TextMode="MultiLine"/>
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        
</table>

