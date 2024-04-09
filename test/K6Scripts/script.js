import http from "k6/http";
import { sleep } from "k6";
import {
  uuidv4,
  randomItem,
  randomString,
  randomIntBetween,
} from "https://jslib.k6.io/k6-utils/1.4.0/index.js";
// https://grafana.com/docs/k6/latest/using-k6/k6-options/reference/#quick-reference-of-options
export const options = {
  // 一个整数值，指定同时运行的 VU 数量，与迭代或持续时间选项一起使用。如果需要更多控制，请查看阶段选项或场景。
  vus: 30,
  // 指定测试运行总持续时间的字符串。在此期间，每个 VU 将循环执行脚本。在 k6 run 和 k6 cloud 命令中可用。
  duration: "5m",
};

// 文名字列表
const chineseNames = [
  "张三",
  "李四",
  "王五",
  "赵六",
  "陈七",
  "周八",
  "吴九",
  "郑十",
  "林华",
  "黄明",
  "朱伟",
  "方强",
  "孙良",
  "胡鹏",
  "郭雷",
  "徐龙",
  "何云",
  "高峰",
  "谢杰",
  "梁宇",
  "许敏",
  "邓刚",
  "曾勇",
  "冯凯",
];

const randWords = `aeioubcdfghijpqrstuv口物音完较两系这己化料期外速相素分爱上邓丽君爱神的箭阿拉斯加的垃圾收到了讲座系列飞机案例介绍反垃圾是否连接起来楼主继续发垃圾啊十分了解`;

const url = "http://localhost:9001/Mobile/Purchase/Submit";

function getRandomAmount(max, min) {
  // 生成随机整数部分
  let integerPart = Math.floor(Math.random() * (max - min + 1)) + min;

  // 生成随机小数部分
  let decimalPart = randomIntBetween(10, 99).toFixed(2);

  // 组合整数部分和小数部分
  let randomNumber = parseFloat(integerPart + "." + decimalPart);

  return randomNumber;
}

export default function () {
  const customerName = randomItem(chineseNames);
  const sellerName = randomItem(chineseNames);
  // 生成随机 GUID
  var payload = JSON.stringify({
    buyer: {
      buyerId: uuidv4(),
      buyerName: customerName,
    },
    seller: {
      sellerId: uuidv4(),
      sellerName: sellerName,
    },
    saleStore: {
      storeId: "AfDf4366-be2A-BDaC-8Ecb-e8EEdCDAA3fA",
      storeName: "京基水贝城市广场黄金旗舰店",
    },
    orderItems: [
      {
        productId: "c2a9B8Fb-e72A-2EB9-e8ed-BC92B5458bEF",
        productName: "手镯" + randomString(2) + "零" + randomString(4),
        unitPrice: getRandomAmount(540, 510),
        laborCost: getRandomAmount(0, 0),
        pictures: [
          {
            url: "http://xomchjsd.mn/mbup",
            description: randomString(10, randWords),
          },
        ],
      },
    ],
    remark: randomString(15, randWords),
  });
  const headers = { "Content-Type": "application/json" };
  // Using a JSON string as body
  http.post(url, payload, { headers });

  sleep(1);
}
