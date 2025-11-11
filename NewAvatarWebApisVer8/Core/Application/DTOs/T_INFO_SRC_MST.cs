using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class T_INFO_SRC_MST
    {
        public int InfoSrcId { get; set; }
        public string InfoSrcName { get; set; }
        public string InfoSrcUrl { get; set; }
        public string InfoSrcDesc { get; set; }
        public char InfoSrcStatus { get; set; }
        public string InfoSrcCtg { get; set; }
        public string InfoSrcTyp { get; set; }
        public char InfoSrcFlag { get; set; }
        public int InfoSrcSortBy { get; set; }
        public int InfoSrcUsrId { get; set; }
        public DateTime InfoSrcEntDt { get; set; }
        public char InfoSrcDownloadSts { get; set; }
        public DateTime InfoSrcCngDt { get; set; }
        public string InfoTypeFlag { get; set; }

    }
}