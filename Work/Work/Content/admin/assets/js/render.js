async function createDataTableFromXMLDataAsync(dataArray) {
    var table = $('.table').DataTable();
   
    table.clear();
    dataArray.forEach(item => {
      var row = [
        item.id,
        item.fullname,
        item.age,
        item.course,
        item.studentCode,
        item.phoneNumber,
        item.studentClass
      ];
  
      table.row.add(row);
    });
    table.draw();
  }
  
  async function fetchDataAndCreateTable() {
    try {
      const response = await fetch('data.xml');
      const xmlText = await response.text();
      const parser = new DOMParser();
      const xmlData = parser.parseFromString(xmlText, 'text/xml');
      const studentElements = xmlData.querySelectorAll('student');
      const xmlDataArray = Array.from(studentElements).map(studentElement => {
        return {
          id: studentElement.querySelector('id').textContent,
          fullname: studentElement.querySelector('fullname').textContent,
          age: studentElement.querySelector('age').textContent,
          course: studentElement.querySelector('course').textContent,
          studentCode: studentElement.querySelector('studentCode').textContent,
          phoneNumber: studentElement.querySelector('phoneNumber').textContent,
          studentClass: studentElement.querySelector('studentClass').textContent
        };
      });
      await createDataTableFromXMLDataAsync(xmlDataArray);
    } catch (error) {
      console.error('Lỗi khi tải dữ liệu XML:', error);
    }
  }
  
  // Gọi hàm để tạo bảng DataTable từ dữ liệu XML bất đồng bộ
  fetchDataAndCreateTable();
  