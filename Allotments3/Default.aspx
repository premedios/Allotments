<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Allotments3._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="FilterContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <label for="companyDropDown">Company</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="companyDropDown" class="form-control" runat="server" OnSelectedIndexChanged="companyDropDown_SelectedIndexChanged" CausesValidation="false" AutoPostBack="true">
                            <asp:ListItem Text="text1" />
                            <asp:ListItem Text="text2" />
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="col">
                <label for="yearDropDown">Year</label>
                <asp:DropDownList ID="yearDropDown" class="form-control" runat="server" OnSelectedIndexChanged="yearDropDown_SelectedIndexChanged" CausesValidation="false" AutoPostBack="true">
                    <asp:ListItem Text="text1" />
                    <asp:ListItem Text="text2" />
                </asp:DropDownList>
            </div>
            <div class="col">
                <label for="monthDropDown">Month</label>
                <asp:DropDownList ID="monthDropDown" class="form-control" runat="server" OnSelectedIndexChanged="monthDropDown_SelectedIndexChanged" CausesValidation="false" AutoPostBack="true">
                    <asp:ListItem Text="text1" />
                    <asp:ListItem Text="text2" />
                </asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
</asp:Content>
