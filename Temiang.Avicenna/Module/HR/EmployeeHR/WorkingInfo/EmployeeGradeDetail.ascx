<%@ Control Language="C#" AutoEventWireup="true" Codebehind="EmployeeGradeDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeGradeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeGrade" runat="server" ValidationGroup="EmployeeGrade" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeGrade"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeGradeID" runat="server" Text="Employee Grade ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtEmployeeGradeID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeGradeID" runat="server" ErrorMessage="Employee Grade ID required."
                    	        ControlToValidate="txtEmployeeGradeID" SetFocusOnError="True" ValidationGroup="EmployeeGrade" Width="100%">
						        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEmployeeGradeMasterID" runat="server" Text="Employee Grade"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEmployeeGradeMasterID" runat="server" Width="300px" 
                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true" 
                            AutoPostBack="false" OnItemDataBound="cboEmployeeGradeMasterID_ItemDataBound"
                            OnItemsRequested="cboEmployeeGradeMasterID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeGradeCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeGradeName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Employee Grade Name required."
            	            ControlToValidate="cboEmployeeGradeMasterID" SetFocusOnError="True" ValidationGroup="EmployeeGrade" Width="100%">
				            <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
			            </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSalaryTableNumber" runat="server" Text="Salary Table Number"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtSalaryTableNumber" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSalaryTableNumber" runat="server" ErrorMessage="Salary Table Number required."
                    	        ControlToValidate="txtSalaryTableNumber" SetFocusOnError="True" ValidationGroup="EmployeeGrade" Width="100%">
						        <asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
			        </td>        
			        <td class="entry">
				        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px">
                   </td>
                    <td>
                    </td>
                </tr>
				<tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeGrade" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="EmployeeGrade"
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
                        <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                    	        ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="EmployeeGrade" Width="100%">
						        <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                    	        ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="EmployeeGrade" Width="100%">
						        <asp:Image ID="Image6" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>        
</table>

<table width="100%">
        
        
        
</table>

