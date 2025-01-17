USE [BookStore]
GO
/****** Object:  Table [dbo].[authors]    Script Date: 6/9/2024 10:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[authors](
	[authorID] [int] IDENTITY(1,1) NOT NULL,
	[authorName] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NULL,
	[phone] [nvarchar](15) NULL,
	[description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[authorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bills]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bills](
	[orderID] [int] NOT NULL,
	[productID] [nvarchar](7) NOT NULL,
	[paymentID] [int] NULL,
	[price] [float] NULL,
	[quantity] [int] NULL,
	[discount] [float] NULL,
 CONSTRAINT [PK_bills] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[categoryID] [nvarchar](2) NOT NULL,
	[categoryName] [nvarchar](255) NOT NULL,
	[description] [text] NULL,
	[status] [bit] NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deliveries]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deliveries](
	[deliveryID] [int] IDENTITY(1,1) NOT NULL,
	[deliveryName] [nvarchar](255) NOT NULL,
	[distance] [float] NULL,
	[price] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[deliveryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQs]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQs](
	[faqID] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[content] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[faqID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[favourite]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[favourite](
	[favouriteID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[productID] [nvarchar](7) NULL,
	[productName] [nvarchar](255) NULL,
	[price] [float] NULL,
	[avatar] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[favouriteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedback]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedback](
	[feedbackID] [int] IDENTITY(1,1) NOT NULL,
	[content] [text] NULL,
	[createAt] [datetime] NULL,
	[numberStars] [int] NULL,
	[userID] [int] NULL,
	[productID] [nvarchar](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[feedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[orderID] [int] IDENTITY(24000000,1) NOT NULL,
	[orderDate] [datetime] NULL,
	[deliveryDate] [datetime] NULL,
	[shipAddress] [nvarchar](255) NULL,
	[note] [nvarchar](255) NULL,
	[userID] [int] NULL,
	[paymentID] [int] NULL,
	[deliveryID] [int] NULL,
 CONSTRAINT [PK__orders__0809337D10B27F60] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payments]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payments](
	[paymentID] [int] IDENTITY(1,1) NOT NULL,
	[paymentName] [nvarchar](255) NOT NULL,
	[description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[paymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[productID] [nvarchar](7) NOT NULL,
	[productName] [nvarchar](255) NOT NULL,
	[description] [text] NULL,
	[avatar] [nvarchar](255) NULL,
	[status] [bit] NULL,
	[price] [float] NULL,
	[sale] [float] NULL,
	[totalStart] [float] NULL,
	[totalFeedback] [int] NULL,
	[totalOrder] [int] NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[categoryID] [nvarchar](2) NULL,
	[subcategoryID] [nvarchar](5) NULL,
	[authorID] [int] NULL,
	[supplierID] [nvarchar](5) NULL,
 CONSTRAINT [PK__products__2D10D14A838853BF] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subcategories]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subcategories](
	[subcategoryID] [nvarchar](5) NOT NULL,
	[subcategoryName] [nvarchar](255) NOT NULL,
	[description] [text] NULL,
	[status] [bit] NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[categoryID] [nvarchar](2) NULL,
	[supplierID] [nvarchar](5) NULL,
 CONSTRAINT [PK__subcateg__E3E4E8D585599B8E] PRIMARY KEY CLUSTERED 
(
	[subcategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[suppliers]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[suppliers](
	[supplierID] [nvarchar](5) NOT NULL,
	[supplierName] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NULL,
	[phone] [nvarchar](15) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK__supplier__DB8E62CD7C11A9BE] PRIMARY KEY CLUSTERED 
(
	[supplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 6/9/2024 10:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[password] [nvarchar](20) NOT NULL,
	[phone] [nvarchar](15) NULL,
	[gender] [bit] NULL,
	[address] [nvarchar](255) NULL,
	[avatar] [nvarchar](255) NULL,
	[status] [bit] NULL,
	[role] [nvarchar](50) NOT NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bills]  WITH CHECK ADD  CONSTRAINT [FK__bills__orderID__4D94879B] FOREIGN KEY([orderID])
REFERENCES [dbo].[orders] ([orderID])
GO
ALTER TABLE [dbo].[bills] CHECK CONSTRAINT [FK__bills__orderID__4D94879B]
GO
ALTER TABLE [dbo].[bills]  WITH CHECK ADD  CONSTRAINT [FK__bills__paymentID__4F7CD00D] FOREIGN KEY([paymentID])
REFERENCES [dbo].[payments] ([paymentID])
GO
ALTER TABLE [dbo].[bills] CHECK CONSTRAINT [FK__bills__paymentID__4F7CD00D]
GO
ALTER TABLE [dbo].[bills]  WITH CHECK ADD  CONSTRAINT [FK__bills__productID__4E88ABD4] FOREIGN KEY([productID])
REFERENCES [dbo].[products] ([productID])
GO
ALTER TABLE [dbo].[bills] CHECK CONSTRAINT [FK__bills__productID__4E88ABD4]
GO
ALTER TABLE [dbo].[favourite]  WITH CHECK ADD  CONSTRAINT [FK__favourite__produ__5629CD9C] FOREIGN KEY([productID])
REFERENCES [dbo].[products] ([productID])
GO
ALTER TABLE [dbo].[favourite] CHECK CONSTRAINT [FK__favourite__produ__5629CD9C]
GO
ALTER TABLE [dbo].[favourite]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[users] ([userID])
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [FK__feedback__produc__59FA5E80] FOREIGN KEY([productID])
REFERENCES [dbo].[products] ([productID])
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [FK__feedback__produc__59FA5E80]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[users] ([userID])
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK__orders__userID__47DBAE45] FOREIGN KEY([userID])
REFERENCES [dbo].[users] ([userID])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK__orders__userID__47DBAE45]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK__products__author__440B1D61] FOREIGN KEY([authorID])
REFERENCES [dbo].[authors] ([authorID])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK__products__author__440B1D61]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK__products__catego__4222D4EF] FOREIGN KEY([categoryID])
REFERENCES [dbo].[categories] ([categoryID])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK__products__catego__4222D4EF]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK__products__subcat__4316F928] FOREIGN KEY([subcategoryID])
REFERENCES [dbo].[subcategories] ([subcategoryID])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK__products__subcat__4316F928]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK__products__suppli__44FF419A] FOREIGN KEY([supplierID])
REFERENCES [dbo].[suppliers] ([supplierID])
ON UPDATE SET NULL
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK__products__suppli__44FF419A]
GO
ALTER TABLE [dbo].[subcategories]  WITH CHECK ADD  CONSTRAINT [FK_subcategories_categories] FOREIGN KEY([categoryID])
REFERENCES [dbo].[categories] ([categoryID])
GO
ALTER TABLE [dbo].[subcategories] CHECK CONSTRAINT [FK_subcategories_categories]
GO
ALTER TABLE [dbo].[subcategories]  WITH CHECK ADD  CONSTRAINT [FK_subcategories_suppliers] FOREIGN KEY([supplierID])
REFERENCES [dbo].[suppliers] ([supplierID])
GO
ALTER TABLE [dbo].[subcategories] CHECK CONSTRAINT [FK_subcategories_suppliers]
GO
