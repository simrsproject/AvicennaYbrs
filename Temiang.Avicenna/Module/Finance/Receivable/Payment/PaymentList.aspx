<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PaymentList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.PaymentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function rowRecalFeePerc(InvoiceNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'recalFeePerc|' + InvoiceNo);
            }

            function pageRecalFeePerc() {
                __doPostBack("<%= grdList.UniqueID %>", 'recalFeePerc|page');
            }

            function rowRecalFeePercByAjax(invoiceNo) {
                if (confirm("Are you sure to recalculate paramedic fee?")) {
                    $.ajax({
                        type: 'POST',
                        url: "<%=Page.ResolveUrl("~/Home/ParSetProporsiJasmed")%>",
                        data: { InvoiceNo: invoiceNo },
                        success: function (data) {
                            //console.log(data);
                            if (data.status === 'OK') {
                                //console.log(data.data);

                            } else {
                                //DisplayToast(ret.data, "error");
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            //DisplayToast(xhr.responseText, "error");
                        },
                        dataType: 'json'
                    });

                    var divCtl = $(document.getElementById(invoiceNo));
                    DisplayStatus(divCtl, '01', 0, '', invoiceNo, 1);
                }
            }

            function DisplayStatus(jQueryDiv, status, progress, errMsg, invoiceNo, iCounter) {
                var txtStatus = "";
                switch (status) {
                    case "01":
                    case "02": {
                        txtStatus = "<img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\cancel16.png")%>\" class=\"icoSpin\" style=\"" + ((status == "01") ? "filter: hue-rotate(120deg)" : "filter: saturate(10)") +"\"> " + progress + "%";
                        //console.log(txtStatus);
                        var iDelay = (iCounter == 0) ? 0 : 3000;
                        // ajax update here
                        setTimeout(function () {
                            iCounter = iCounter + 1;
                            $.ajax({
                                type: 'POST',
                                url: "<%=Page.ResolveUrl("~/Home/ParGetProgressProporsiJasmed")%>",
                                data: { InvoiceNo: invoiceNo },
                                success: function (data) {
                                    //console.log(data);
                                    if (data.status === 'OK') {
                                        //console.log(data.data);

                                        DisplayStatus(
                                            jQueryDiv,
                                            data.data.SRPhysicianFeeProportionalStatus,
                                            data.data.PhysicianFeeProportionalPercentage,
                                            data.data.PhysicianFeeProportionalErrMessage,
                                            invoiceNo, iCounter);
                                    } else {
                                        //DisplayToast(ret.data, "error");
                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    //DisplayToast(xhr.responseText, "error");
                                },
                                dataType: 'json'
                            });
                        }, iDelay);
                        break;
                    } case "04": {
                        txtStatus = "<a href=\"#\" onclick=\"rowRecalFeePercByAjax('" + invoiceNo + "'); return false;\"><img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\refresh16.png")%>\" class=\"grayscale\" title=\"Error: " + errMsg + "\" ></a>";
                        break;
                    } default: {
                        txtStatus = "<a href=\"#\" onclick=\"rowRecalFeePercByAjax('" + invoiceNo + "'); return false;\"><img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\refresh16.png")%>\" ></a>";
                        break;
                    }
                }
                jQueryDiv.html(txtStatus);
            }

            function RowCreated(sender, eventArgs) {
                var hasAccess = '<%= HasAccessToPhysicianFee() %>' == 'True';

                if (!hasAccess) return;

                //console.log(hasAccess);
                //console.log("Row with ID: " + eventArgs.get_gridDataItem().get_element().rowIndex + " was created");
                //console.log(eventArgs.get_gridDataItem().get_element().cells[1].textContent);
                //console.log(eventArgs.get_gridDataItem().get_element().cells);
                //console.log(eventArgs.getDataKeyValue("InvoiceNo"));

                var parID = eventArgs.get_gridDataItem().get_element().cells[1].textContent;
                //console.log(parID);

                if (parID.includes("PM")) return;


                var parStatus = '01';//eventArgs.get_gridDataItem().get_element().cells[8].textContent;
                var parProgress = 0;//eventArgs.get_gridDataItem().get_element().cells[9].textContent;

                var divCtl = $(document.getElementById(parID));
                //console.log(divCtl);

                //console.log(ParStatus);

                DisplayStatus(divCtl, parStatus, parProgress, '', parID, 0);
            }
            //function RowDataBound(sender, args) {
            //    console.log("cc");
            //}

            //function RowShown(sender, eventArgs) {
            //    console.log("Row: " + eventArgs.get_itemIndexHierarchical() + " was shown");
            //}
        </script>
    </telerik:RadCodeBlock>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDetailTableDataBind="grdList_DetailTableDataBind" 
        AllowPaging="true" AllowCustomPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="InvoiceNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceNo" HeaderText="Payment No"
                    UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate" HeaderText="Payment Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceReferenceNo" HeaderText="Invoice No"
                    UniqueName="InvoiceReferenceNo" SortExpression="InvoiceReferenceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalAmount" HeaderText="Total Amount"
                    UniqueName="TotalAmount" SortExpression="TotalAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ReceivableStatus"
                    HeaderText="Status" UniqueName="ReceivableStatus" SortExpression="ReceivableStatus"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved"
                    HeaderText="Approved" UniqueName="IsApproved" SortExpression="IsApproved"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="SRPhysicianFeeProportionalStatus" HeaderText=""
                    UniqueName="SRPhysicianFeeProportionalStatus" SortExpression="SRPhysicianFeeProportionalStatus" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="true" />
                <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="PhysicianFeeProportionalPercentage" HeaderText="PhysicianFeeProportionalPercentage"
                    UniqueName="PhysicianFeeProportionalPercentage" SortExpression="PhysicianFeeProportionalPercentage" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="true" />


                <telerik:GridTemplateColumn UniqueName="RecalFeePerc" HeaderText="Recalculate Percentage Fee Payment" Groupable="false" Visible="false">
                    <ItemTemplate>
                        <%# !HasAccessToPhysicianFee() ? "" : string.Format("<a href=\"#\" onclick=\"rowRecalFeePerc('{0}'); return false;\">{1}</a>",
                            DataBinder.Eval(Container.DataItem, "InvoiceNo"),
                            "<img src=\"../../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Recalculate Percentage Fee Payment \" />")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <HeaderTemplate>
                        <%# !HasAccessToPhysicianFee() ? "" : string.Format("<a href=\"#\" onclick=\"pageRecalFeePerc(); return false;\">{0}</a>",
                            "<img src=\"../../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Recalculate Percentage Fee Payment\" />")%>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                
                <telerik:GridTemplateColumn UniqueName="FeeProgress" HeaderText="Fee Control" Groupable="false" >
                    <ItemTemplate>
                        <%# !HasAccessToPhysicianFee() ? "" : string.Format("<div id=\"{0}\" />", DataBinder.Eval(Container.DataItem, "InvoiceNo").ToString())%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="InvoiceNo, PaymentNo, InvoiceReferenceNo" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceReferenceNo" HeaderText="Invoice No"
                            UniqueName="InvoiceReferenceNo" SortExpression="InvoiceReferenceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                            UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            HeaderStyle-Width="80px" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VerifyAmount" HeaderText="Amount"
                            UniqueName="VerifyAmount" SortExpression="VerifyAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PaymentAmount" HeaderText="Payment Amount"
                            UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OtherAmount" HeaderText="Discount"
                            UniqueName="OtherAmount" SortExpression="OtherAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="DiscountReason" HeaderText="Discount Reason"
                            UniqueName="DiscountReason" SortExpression="DiscountReason" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BankCost" HeaderText="Bank Cost"
                            UniqueName="BankCost" SortExpression="OtherAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings>
            <ClientEvents OnRowCreated="RowCreated" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
