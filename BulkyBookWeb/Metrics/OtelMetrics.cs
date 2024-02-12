using System.Diagnostics.Metrics;

namespace BulkyBookWeb.Metrics
{
    public class OtelMetrics
    {
        //Books meters
        private Counter<int> BooksAddedCounter { get; }
        private Counter<int> BooksDeletedCounter { get; }
        private Counter<int> BooksUpdatedCounter { get; }
        private ObservableGauge<int> TotalBooksGauge { get; }
        private int _totalBooks = 0;

        private Histogram<int> NumberOfBooksPerOrderHistogram { get; }

        public string MetricName { get; }

        public OtelMetrics(string meterName = "BulkyBook")
        {
            var meter = new Meter(meterName);
            MetricName = meterName;

            BooksAddedCounter = meter.CreateCounter<int>("books-added", "Book");
            BooksDeletedCounter = meter.CreateCounter<int>("books-deleted", "Book");
            BooksUpdatedCounter = meter.CreateCounter<int>("books-updated", "Book");
            TotalBooksGauge = meter.CreateObservableGauge<int>("total-books", () => new[] { new Measurement<int>(_totalBooks) });
            NumberOfBooksPerOrderHistogram = meter.CreateHistogram<int>("orders-number-of-books", "Books", "Number of books per order");
        }

        //Books meters
        public void AddBook() => BooksAddedCounter.Add(1);
        public void DeleteBook() => BooksDeletedCounter.Add(1);
        public void UpdateBook() => BooksUpdatedCounter.Add(1);
        public void IncreaseTotalBooks() => _totalBooks++;
        public void DecreaseTotalBooks() => _totalBooks--;

        public void RecordNumberOfBooks(int amount) => NumberOfBooksPerOrderHistogram.Record(amount);
    }
}
