<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PersonalEmergencyContactDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalEmergencyContactDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalEmergencyContact" runat="server" ValidationGroup="PersonalEmergencyContact" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalEmergencyContact"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>


<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPersonalEmergencyContactID" runat="server" Text="Personal Emergency Contact ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPersonalEmergencyContactID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonalEmergencyContactID" runat="server" ErrorMessage="Personal Emergency Contact ID required."
                    	        ControlToValidate="txtPersonalEmergencyContactID" SetFocusOnError="True" ValidationGroup="PersonalEmergencyContact" Width="100%">
						        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRFamilyRelation" runat="server" Text="Family Relation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRFamilyRelation" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRFamilyRelation" runat="server" ErrorMessage="Family Relation required."
                            ControlToValidate="cboSRFamilyRelation" SetFocusOnError="True" ValidationGroup="PersonalEmergencyContact"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblContactName" runat="server" Text="Contact Name"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtContactName" runat="server" Width="300px" MaxLength="100"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ErrorMessage="Contact Name required."
                    	        ControlToValidate="txtContactName" SetFocusOnError="True" ValidationGroup="PersonalEmergencyContact" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtPhone" runat="server" Width="300px" MaxLength="20"/>
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtMobile" runat="server" Width="300px" MaxLength="20"/>
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                    </td>
                </tr>                
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" Height="80px" MaxLength="400" TextMode="MultiLine"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Address required."
                    	        ControlToValidate="txtAddress" SetFocusOnError="True" ValidationGroup="PersonalEmergencyContact" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
				<tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalEmergencyContact" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalEmergencyContact"
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
                        <asp:Label ID="Label1" runat="server" Text="Zip Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboZipCode" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboZipCode_ItemDataBound"
                            OnItemsRequested="cboZipCode_ItemsRequested" OnSelectedIndexChanged="cboZipCode_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "District")%>
                                &nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ZipPostalCode")%>) </b>
                                <br />
                                County :
                                <%# DataBinder.Eval(Container.DataItem, "County")%>
                                <br />
                                City :
                                <%# DataBinder.Eval(Container.DataItem, "City")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDistrict" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCounty" runat="server" Text="County"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCounty" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCity" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRState" runat="server" Text="Province"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRState" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRState" runat="server" ErrorMessage="Province required."
                            ControlToValidate="cboSRState" SetFocusOnError="True" ValidationGroup="PersonalEmergencyContact" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>