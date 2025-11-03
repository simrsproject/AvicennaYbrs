<%@ Page Title="Fee Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="ParamedicFeePaymentDetail.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeePaymentV3.ParamedicFeePaymentDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function rowRecalFeePercByAjax(keyId, invoiceNo) {
                if (confirm('Are you sure want to recalculate payment percentage?')) {
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

                    var divCtl = $(document.getElementById(keyId));
                    //console.log(divCtl.attr("group"));
                    var par = divCtl.attr("group");

                    var divCtl = $(document.getElementById(invoiceNo));
                    DisplayStatus(keyId, divCtl, '01', 0, '', par, 1);
                }

                return false;
            }

            function DisplayStatus(keyId, jQueryDiv, status, progress, errMsg, invoiceNo, iCounter) {
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
                                            keyId,
                                            jQueryDiv,
                                            data.data.SRPhysicianFeeProportionalStatus,
                                            data.data.PhysicianFeeProportionalPercentage,
                                            data.data.PhysicianFeeProportionalErrMessage,
                                            invoiceNo, iCounter);
                                    } else {
                                        txtStatus = "";
                                        jQueryDiv.html(txtStatus);
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
                        txtStatus = "<a href=\"#\" onclick=\"return rowRecalFeePercByAjax('" + keyId + "','" + invoiceNo + "');\"><img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\refresh16.png")%>\" class=\"grayscale\" title=\"Error: " + errMsg + "\" ></a>";
                        break;
                    } default: {
                        txtStatus = "<a href=\"#\" onclick=\"return rowRecalFeePercByAjax('" + keyId + "','" + invoiceNo + "');\"><img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\refresh16.png")%>\" ></a>";
                        break;
                    }
                }
                jQueryDiv.html(txtStatus);
            }

            function RowCreated(sender, eventArgs) {
                var IsPropor = '<%= IsFeeCalculateProporsionalOnPayment() %>' == 'True';

                if (!IsPropor) return;

                var keyId = eventArgs.get_gridDataItem().get_element().cells[0].textContent;
                //console.log(keyId);

                var parStatus = '01';//eventArgs.get_gridDataItem().get_element().cells[8].textContent;
                var parProgress = 0;//eventArgs.get_gridDataItem().get_element().cells[9].textContent;

                var divCtl = $(document.getElementById(keyId));
                //console.log(divCtl.attr("group"));
                var par = divCtl.attr("group");

                //$("[id=choose]")

                //console.log(ParStatus);

                DisplayStatus(keyId, divCtl, parStatus, parProgress, '', par, 0);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnItemDataBound="grdList_ItemDataBound" 
        AllowPaging="False" AllowSorting="true" ShowStatusBar="true">
        <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" AutoGenerateColumns="false">
            <Columns>
                <telerik:GridBoundColumn DataField="Id" HeaderText="" UniqueName="KeyId"
                    SortExpression="Id" >
                    <HeaderStyle HorizontalAlign="Left" Width="0px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Percentage" HeaderText="Prctg" UniqueName="Percentage"
                    SortExpression="Percentage" DataFormatString="{0:n2}" >
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" >
                    <HeaderStyle HorizontalAlign="Left"/>
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="RegistrationNo" UniqueName="RegistrationNo"
                    SortExpression="RegistrationNo" >
                    <HeaderStyle HorizontalAlign="Left" Width="140px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FeeAmount" HeaderText="Fee Amount" UniqueName="FeeAmount"
                    SortExpression="FeeAmount" DataFormatString="{0:n2}" >
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" BackColor="LightBlue" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                    SortExpression="Price" DataFormatString="{0:n2}" >
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="PctgFee" HeaderText="Pctg Calc">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "PctgFee")).ToString("n2") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="PaymentRefNoRecal" HeaderText="Payment No" SortExpression="PaymentRefNo">
                    <ItemTemplate>
                        <div><span><%# DataBinder.Eval(Container.DataItem, "PaymentRefNo")%></span>
                            <%# string.Format("<div id=\"{0}\" group=\"{1}\" />", DataBinder.Eval(Container.DataItem, "Id").ToString(), DataBinder.Eval(Container.DataItem, "PaymentRefNo").ToString()) %>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="160px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="PaymentRefDate" DataType="System.DateTime"
                    DataFormatString="{0:dd MMMM yyyy hh:mm tt}" HeaderStyle-Width="80px"
                    HeaderText="Payment Date" SortExpression="PaymentRefDate" UniqueName="PaymentRefDate"> 
                </telerik:GridDateTimeColumn> 
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" >
                    <HeaderStyle HorizontalAlign="Left"/>
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowCreated="RowCreated" />
        </ClientSettings>
    </telerik:RadGrid>
    <br /><br /><br />
</asp:Content>