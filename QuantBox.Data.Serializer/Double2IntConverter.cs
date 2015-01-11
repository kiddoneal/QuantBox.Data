﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantBox.Data.Serializer
{
    public class Double2IntConverter
    {
        private bool flat = true;
        private PbTick tmpTick = new PbTick();
        public Double2IntConverter(bool flat)
        {
            this.flat = flat;
        }

        public PbTickCodec Codec;
        public BarInfo Double2Int(BarInfoView bar)
        {
            if (bar == null)
                return null;

            BarInfo field = new BarInfo();
            
            field.Open = Codec.PriceToTick(bar.Open);
            field.High = Codec.PriceToTick(bar.High);
            field.Low = Codec.PriceToTick(bar.Low);
            field.Close = Codec.PriceToTick(bar.Close);
            field.BarSize = bar.BarSize;

            return field;
        }

        public StaticInfo Double2Int(StaticInfoView Static)
        {
            if (Static == null)
                return null;

            StaticInfo field = new StaticInfo();

            field.LowerLimitPrice = Codec.PriceToTick(Static.LowerLimitPrice);
            field.UpperLimitPrice = Codec.PriceToTick(Static.UpperLimitPrice);

            if (flat)
            {
                field.SettlementPrice = Codec.PriceToTick(Static.SettlementPrice);
            }
            else
            {
                Codec.SetSettlementPrice(tmpTick, Static.SettlementPrice);

                field.SettlementPrice = tmpTick.Static.SettlementPrice;
            }

            field.Symbol = Static.Symbol;
            field.Exchange = Static.Exchange;
            field.Multiplier = Static.Multiplier;

            return field;
        }

        public DepthTick Double2Int(DepthTickView depthTick)
        {
            if (depthTick == null)
                return null;

            DepthTick field = new DepthTick();

            field.BidPrice1 = Codec.PriceToTick(depthTick.BidPrice1);
            field.BidSize1 = depthTick.BidSize1;
            field.AskPrice1 = Codec.PriceToTick(depthTick.AskPrice1);
            field.AskSize1 = depthTick.AskSize1;
            field.BidPrice2 = Codec.PriceToTick(depthTick.BidPrice2);
            field.BidSize2 = depthTick.BidSize2;
            field.AskPrice2 = Codec.PriceToTick(depthTick.AskPrice2);
            field.AskSize2 = depthTick.AskSize2;
            field.BidPrice3 = Codec.PriceToTick(depthTick.BidPrice3);
            field.BidSize3 = depthTick.BidSize3;
            field.AskPrice3 = Codec.PriceToTick(depthTick.AskPrice3);
            field.AskSize3 = depthTick.AskSize3;

            if (depthTick.Next != null)
                field.Next = Double2Int(depthTick.Next);

            field.BidCount1 = depthTick.BidCount1;
            field.AskCount1 = depthTick.AskCount1;
            field.BidCount2 = depthTick.BidCount2;
            field.AskCount2 = depthTick.AskCount2;
            field.BidCount3 = depthTick.BidCount3;
            field.AskCount3 = depthTick.AskCount3;

            return field;
        }

        public PbTick Double2Int(PbTickView tick)
        {
            if (tick == null)
                return null;

            PbTick field = new PbTick();

            // 利用此机会设置TickSize
            if (Codec == null)
            {
                Codec = new PbTickCodec();
                if (flat)
                {
                    Codec.TickSize = 1.0;
                }
                else
                {
                    Codec.TickSize = tick.TickSize;
                }
            }

            if (flat)
            {
                field.TickSize = (int)tick.TickSize;
                field.Turnover = (long)tick.Turnover;
                field.AveragePrice = (int)tick.AveragePrice;
            }
            else
            {
                Codec.SetTickSize(tmpTick, tick.TickSize);
                Codec.SetTurnover(tmpTick, tick.Turnover);
                Codec.SetAveragePrice(tmpTick, tick.AveragePrice);

                field.TickSize = tmpTick.TickSize;
                field.Turnover = tmpTick.Turnover;
                field.AveragePrice = tmpTick.AveragePrice;
            }

            field.LastPrice = Codec.PriceToTick(tick.LastPrice);

            field.Depth1_3 = Double2Int(tick.Depth1_3);
            field.Volume = tick.Volume;
            field.OpenInterest = tick.OpenInterest;
            
            field.TradingDay = tick.TradingDay;
            field.ActionDay = tick.ActionDay;
            field.Time_HHmm = tick.Time_HHmm;
            field.Time_____ssf__ = tick.Time_____ssf__;
            field.Time________ff = tick.Time________ff;
            field.Time_ssf_Diff = tick.Time_ssf_Diff;

            field.Bar = Double2Int(tick.Bar);
            field.Static = Double2Int(tick.Static);



            return field;
        }
    }
}
